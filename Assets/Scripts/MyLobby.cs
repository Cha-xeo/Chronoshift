using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;
using AuthenticationException = Unity.Services.Authentication.AuthenticationException;

namespace Chronoshift.Multiplayer
{
    public class MyLobby : MonoBehaviour
    {
        [SerializeField] TMP_InputField tmp;
        [SerializeField] TMP_InputField playerNameText;
        [SerializeField] Transform _lobbyList;
        [SerializeField] GameObject _lobbyPrefab;
        [SerializeField] GameObject _lobbyCanvas;
        public const string KEY_PLAYER_NAME = "PlayerName";
        public const string KEY_PLAYER_READY = "Ready";
        [SerializeField] GameObject _lobbyWaitingCanvas;
        [SerializeField] Sprite _lobbyCheckMark;
        [SerializeField] Transform _PlayerWaintingParent;
        [SerializeField] GameObject _PlayerWaintingPrefab;
        [SerializeField] Button _PlayerWaintingStartButtoon;
        bool _isLeader = false;
        public bool _isWaitingPlayers = false;

        Lobby joinedLobby;
        List<Lobby> _lobbyes;
        float hearbeat;
        bool _connected = false;
        bool _bothReady = false;
        bool _isReady = false;
        string _playerName;
        private float lobbyPollTimer;
        async void Start()
        {
            await UnityServices.InitializeAsync();
        }
        public async void Authenticate()
        {
            _playerName = playerNameText.text;
            Debug.Log(_playerName);
            Debug.Log("Text " +playerNameText.text);
            InitializationOptions initializationOptions = new InitializationOptions();
            initializationOptions.SetProfile(_playerName);

            await UnityServices.InitializeAsync(initializationOptions);

            AuthenticationService.Instance.SwitchProfile(_playerName);
            Debug.Log(AuthenticationService.Instance.Profile);
            AuthenticationService.Instance.SignedIn += () => {
                // do nothing
                Debug.Log("Signed in! " + AuthenticationService.Instance.PlayerId);
            };
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            catch (AuthenticationException e)
            {
                Debug.Log(e);
            }
        }
        public void PlayerWaiting()
        {
            _isWaitingPlayers = !_isWaitingPlayers;
            _lobbyWaitingCanvas.SetActive(true);
            _lobbyCanvas.SetActive(false);
            RefreshWaitingLobby();
        }
        private void Update()
        {
            // Keep the lobby alive for more than 30s
            HandleHearthbeat();
            HandleLobbyPolling();
        }
        private async void HandleLobbyPolling()
        {
            if (joinedLobby != null)
            {
                lobbyPollTimer -= Time.deltaTime;
                if (lobbyPollTimer < 0f)
                {
                    float lobbyPollTimerMax = 1.5f;
                    lobbyPollTimer = lobbyPollTimerMax;
                    if (!_isWaitingPlayers)
                    {
                        joinedLobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);

                        if (!IsPlayerInLobby())
                        {
                            // Player was kicked out of this lobby
                            Debug.Log("Kicked from Lobby!");
                            joinedLobby = null;
                        }
                    }else{
                        RefreshWaitingLobby();
                    }
                }
            }
        }

        private bool IsPlayerInLobby()
        {
            if (joinedLobby != null && joinedLobby.Players != null)
            {
                foreach (Player player in joinedLobby.Players)
                {
                    if (player.Id == AuthenticationService.Instance.PlayerId)
                    {
                        // This player is in this lobby
                        return true;
                    }
                }
            }
            return false;
        }

        private async void HandleHearthbeat()
        {
            if (joinedLobby != null && _isLeader)
            {
                hearbeat -= Time.deltaTime;
                if (hearbeat < 0f)
                {
                    hearbeat = 25f;
                    
                    await LobbyService.Instance.SendHeartbeatPingAsync(joinedLobby.Id);

                }
            }
        }
        public async void CreateLobby()
        {
            if (_connected) return;
            try
            {
                int maxPlayers = 2;
                Lobby _lobby = await LobbyService.Instance.CreateLobbyAsync(tmp.text, maxPlayers);
                joinedLobby = _lobby;
                Debug.Log("Lobby: " + _lobby.Name + " " + _lobby.MaxPlayers);
                _isLeader = true;
                PlayerJoinLobby();
                PlayerWaiting();
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }

        public async Task<List<Lobby>> ListLobbies()
        {
            try
            {
                // Todo querry filter
                QueryResponse query = await Lobbies.Instance.QueryLobbiesAsync();

                Debug.Log("Lobbies found: " + query.Results.Count);
                foreach (Lobby result in query.Results)
                {
                    Debug.Log(result.Name + " " + result.MaxPlayers);
                }
                return query.Results;
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
            return null;

        }

        public async void QuickJoin()
        {
            try
            {
                // Todo querry filter
                joinedLobby =  await LobbyService.Instance.QuickJoinLobbyAsync();
                PlayerJoinLobby();
                PlayerWaiting();
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }

        }
        public async void DeleteLooby(string id)
        {
            try
            {
                await Lobbies.Instance.DeleteLobbyAsync(id);

            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
        public async void QuitLooby()
        {
            if (!_connected) return;
            try
            {
                //Ensure you sign-in before calling Authentication Instance
                //See IAuthenticationService interface
                string playerId = AuthenticationService.Instance.PlayerId;
                await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, playerId);
                _connected = false;
                _isLeader = false;
                if (_isLeader) DeleteLooby(joinedLobby.Id);
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }

        public async void JoinLobbieWithId(string id)
        {
            try
            {
                _connected = true;
                joinedLobby = await Lobbies.Instance.JoinLobbyByIdAsync(id);
                PlayerJoinLobby();
                PlayerWaiting();
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }

        public async void OnReadyClick()
        {
            if (joinedLobby != null)
            {
                try
                {
                    _isReady = !_isReady;
                    UpdatePlayerOptions options = new UpdatePlayerOptions();

                    options.Data = new Dictionary<string, PlayerDataObject>() {
                        {

                            KEY_PLAYER_READY, new PlayerDataObject(
                                visibility: PlayerDataObject.VisibilityOptions.Public,
                                value: _isReady.ToString())
                        }
                    };
                    string playerId = AuthenticationService.Instance.PlayerId;
                    joinedLobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, playerId, options); ;
                }
                catch (LobbyServiceException e)
                {
                    Debug.Log(e);
                }
            }
        }
        private async void PlayerJoinLobby()
        {
            if (joinedLobby != null) {
                try
                {
                    UpdatePlayerOptions options = new UpdatePlayerOptions();

                    options.Data = new Dictionary<string, PlayerDataObject>() {
                        {
                            KEY_PLAYER_NAME, new PlayerDataObject(
                            visibility: PlayerDataObject.VisibilityOptions.Public,
                            value: _playerName)
                        },{

                            KEY_PLAYER_READY, new PlayerDataObject(
                                visibility: PlayerDataObject.VisibilityOptions.Public,
                                value: _isReady.ToString())
                        }
                    };
                    string playerId = AuthenticationService.Instance.PlayerId;
                    joinedLobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, playerId, options); ;
                }
                catch (LobbyServiceException e)
                {
                    Debug.Log(e);
                }
            }
        }

        public async void RefreshWaitingLobby()
        {
            if (joinedLobby != null)
            {
                joinedLobby = await Lobbies.Instance.GetLobbyAsync(joinedLobby.Id);
                foreach (Transform child in _PlayerWaintingParent)
                {
                    Destroy(child.gameObject);
                }
                foreach (Player item in joinedLobby.Players)
                {
                    GameObject player = Instantiate(_PlayerWaintingPrefab, _PlayerWaintingParent);
                    player.GetComponentInChildren<TMP_Text>().text = item.Data[KEY_PLAYER_NAME].Value;
                    switch (item.Data[KEY_PLAYER_READY].Value)
                    {
                        case "True":
                            player.GetComponentInChildren<Image>().enabled = true;
                            _bothReady = true;
                            // player ready
                            break;
                        default:
                            player.GetComponentInChildren<Image>().enabled = false;
                            _bothReady = false;
                            // player not ready
                            break;
                    }
                }

                if (_isLeader && _bothReady)
                {
                    _PlayerWaintingStartButtoon.gameObject.SetActive(true);
                }
            }
        }
        public async void UpdatePlayerName()
        {
            if (joinedLobby != null)
            {
                try
                {
                    UpdatePlayerOptions options = new UpdatePlayerOptions();

                    options.Data = new Dictionary<string, PlayerDataObject>() {
                    {
                        KEY_PLAYER_NAME, new PlayerDataObject(
                            visibility: PlayerDataObject.VisibilityOptions.Public,
                            value: _playerName)
                    }
                };

                    string playerId = AuthenticationService.Instance.PlayerId;

                    joinedLobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, playerId, options);

                }
                catch (LobbyServiceException e)
                {
                    Debug.Log(e);
                }
            }
        }

        public async void RefreshLobby()
        {
            foreach (Transform child in _lobbyList)
            {
                Destroy(child.gameObject);
            }
            _lobbyes = await ListLobbies();

            foreach (Lobby item in _lobbyes)
            {
                GameObject lob = Instantiate(_lobbyPrefab, _lobbyList);
                lob.transform.GetChild(0).GetComponent<TMP_Text>().text = item.Name;
                lob.transform.GetChild(1).GetComponent<TMP_Text>().text = item.Players.Count + "/" + item.MaxPlayers;
                lob.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => JoinLobbieWithId(item.Id));
                //lob.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(PlayerWaiting);
            }
        }

    }
}
