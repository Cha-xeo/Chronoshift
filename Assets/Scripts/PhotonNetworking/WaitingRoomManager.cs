using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chronoshift.PhotonNetworking
{
    public class WaitingRoomManager : MonoBehaviourPunCallbacks
    {
        int _playerCount;
        int _roomSize;
        [SerializeField] TMP_Text _plaersText;
        [SerializeField] TMP_Text _TimerText;
        [SerializeField] PhotonView _view;
        [SerializeField] GameObject _waitingCanvas;
        bool _readyToStart = false;
        bool _startingGame = false;
        float _timer;
        public float maxTimer;


        private void Start()
        {
            _timer = maxTimer;
            PlayerCountUpdate();
        }

        private void Update()
        {
            WaitingForMorePlayers();
        }

        void WaitingForMorePlayers()
        {
            if (_playerCount <= 1)
            {
                ResetTimer();
            }

            if (_readyToStart)
            {
                _timer -= Time.deltaTime;
            }

            string tmpTimer = string.Format("{0:00}", _timer);
            _TimerText.text = tmpTimer;

            if (_timer <= 0f)
            {
                if (_startingGame) return;
                StartGame();
            }
        }

        private void ResetTimer()
        {
            _timer = maxTimer;
        }

        private void StartGame()
        {
            Debug.Log(PhotonNetwork.AutomaticallySyncScene);
            PhotonNetwork.AutomaticallySyncScene = true;
            _startingGame = true;
            if (!PhotonNetwork.IsMasterClient)
            {
                _waitingCanvas.SetActive(false);
                gameObject.SetActive(false);
                return;
            }
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel("Game");
        }

        void PlayerCountUpdate()
        {
            _playerCount = PhotonNetwork.PlayerList.Length;
            _roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
            _plaersText.text = _playerCount + ":" + _roomSize;
            if (_playerCount == _roomSize)
            {
                _readyToStart = true;
            }
            else
            {
                _readyToStart = false;
                _startingGame = false;
            }
        }

        public void DelayCancel()
        {
            PhotonNetwork.LeaveLobby();
            SceneManager.LoadScene("PhotonLobbyScene");
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            PlayerCountUpdate();
            if (PhotonNetwork.IsMasterClient)
            {
                _view.RPC("RPC_SendTimer", RpcTarget.Others, _timer);
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            PlayerCountUpdate();
        }

        [PunRPC]
        void RPC_SendTimer(float timr)
        {
            _timer = timr;
        }
    }
}
