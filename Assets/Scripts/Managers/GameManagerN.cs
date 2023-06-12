using Chronoshift.PlayerController;
using Chronoshift.UI;
using Photon.Pun;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Grid = Chronoshift.Tiles.Grid;
namespace Chronoshift.Managers
{
    public enum GameStateN
    {
        SelectElement,
        GenerateGrid,
        Player1Turn,
        Player2Turn,
        Chronoshift,
        GameOver
    }

    public class GameManagerN : MonoBehaviour
    {
        [SerializeField] GameObject SpellHUD;
        [SerializeField] GameObject SelectHUD;
        [SerializeField] ElementSeletionUI el;
        [SerializeField] PhotonView _view;
        [SerializeField] Image _image;
        [SerializeField] TMP_Text TurnText;
        [SerializeField] Button SpellButton;
        [SerializeField] Button UltButton;
        public GameStateN _state;
        int playerReady = 0;
        public Constants.Elements StarterElement;
        public float Timer = 20f;
        private static GameManagerN instance;

        public static GameManagerN Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameManagerN>();
                }

                return instance;
            }
        }

        void DoStuff()
        {
            _image.fillAmount = 1;
            PlayerNController.Instance.IsPlaying = false;
            switch (_state)
            {
                case GameStateN.Player1Turn:
                    ChangeState(GameStateN.Player2Turn);
                    break;
                case GameStateN.Player2Turn:
                    ChangeState(GameStateN.Chronoshift);
                    break;
                case GameStateN.Chronoshift:
                    ChangeState(GameStateN.Player1Turn);
                    break;
            }

        }

        IEnumerator Countdown(float seconds)
        {
            /*float duration = seconds;
            float totalTime = 0f;
            while (duration > 0)
            {
                _image.fillAmount = duration / seconds;
                duration -= Time.deltaTime;
                yield return null;
            }
            DoStuff();*/
                yield return null;
        }

        public void PunChangeState(GameStateN state)
        {
            _view.RPC("RPC_ChangeState", RpcTarget.AllViaServer, state);
        }

        void StopTimer()
        {
            StopCoroutine("Countdown");
            _image.fillAmount = 1;
        }

        public void ChangeState(GameStateN state)
        {
            _state = state;
            switch (_state)
            {
                case GameStateN.SelectElement:
                    break;
                case GameStateN.GenerateGrid:
                    GridGenerator.Instance.CreateWorld();
                    break;
                case GameStateN.Player1Turn:
                    StopTimer();
                    StartCoroutine(Countdown(Timer));
                    if (!PhotonNetwork.IsMasterClient)
                    {
                        YourTurn();
                        return;
                    }
                    EnnemyTurn();
                    break;
                case GameStateN.Player2Turn:
                    StopTimer();
                    StartCoroutine(Countdown(Timer));
                    if (PhotonNetwork.IsMasterClient)
                    {
                        YourTurn();
                        return;
                    }
                    EnnemyTurn();
                    break;
                case GameStateN.Chronoshift:
                    RewindTurn();
                    break;
                case GameStateN.GameOver:
                    break;
            }
        }

        void YourTurn()
        {
            TurnText.text = Constants.YTURN;
            SpellButton.interactable = true;
            UltButton.interactable = true;
            PlayerNController.Instance.IsPlaying = true;
            PlayerNController.Instance.mode = Mode.Move;
        }

        void EnnemyTurn()
        {
            TurnText.text = Constants.ETURN;
            SpellButton.interactable = false;
            UltButton.interactable = false;
            PlayerNController.Instance.mode = Mode.Blocked;
            PlayerNController.Instance.IsPlaying = false;
        }

        void RewindTurn()
        {
            TurnText.text = Constants.RTURN;
            SpellButton.interactable = false;
            UltButton.interactable = false;
            PlayerNController.Instance.mode = Mode.Blocked;
            _image.fillAmount = 1;
            ChronoNManager.Instance.StartChronoshitLocal();
        }

        public void DecreaseTimer(float amount)
        {
            _image.fillAmount = amount;
        }

        public void OnReadyClick()
        {
            GridGenerator.Instance.ConfirmAndUpdateProperties();
            SelectHUD.SetActive(false);
            _view.RPC("RPC_SendReady", RpcTarget.MasterClient);
        }

        [PunRPC]
        void RPC_SendReady()
        {
            playerReady++;
            if (playerReady == 2)
            {
                _view.RPC("StartRound", RpcTarget.AllViaServer);
            }
        }
        [PunRPC]
        void StartRound()
        {
            StarterElement = el.Element.Elements;
            SpellHUD.SetActive(true);
            ChangeState(GameStateN.GenerateGrid);
        }

        [PunRPC]
        void RPC_ChangeState(GameStateN state)
        {
            ChangeState(state);
        }
    }
}
