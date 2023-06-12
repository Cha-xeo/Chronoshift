using Photon.Pun;
using UnityEngine;
using System;
using Chronoshift.Tiles;
using Chronoshift.Managers;
using UnityEngine.Rendering.Universal;
using Grid =  Chronoshift.Tiles.Grid;
using System.Linq;
using System.Collections.Generic;

namespace Chronoshift.PlayerController
{
    public enum Mode
    {
        Blocked,
        Move,
        Casting
    }

    public class PlayerNController : MonoBehaviourPunCallbacks
    {
        Mode _mode;
        public bool IsPlaying = false;
        public int currentTile;
        public float moveRange = 3;
        public float CastRange = 6;
        public Dictionary<int, Tile> InRangeTile = new();
        public LayerMask tileLayer;

        private static PlayerNController instance;

        [SerializeField] Transform playerParent;
        GameObject _playerInstance;
        public Character1 PlayerScript;

        [SerializeField] GameObject GlobalLight;
        [SerializeField] Light2D Light;
        [HideInInspector] public PhotonView PlayerView;
        public static PlayerNController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PlayerNController>();
                }

                return instance;
            }
        }

        public Mode mode { get => _mode; 
            set {
                if (!PlayerView.IsMine) return;
                _mode = value;
                ResetShowedTile();
                DisplayRange();
            }
        }
        public int manamax = 4;
        int _mana;
        public int mana { 
            get => _mana; 
            set{
                if (!PlayerView.IsMine) return;
                _mana = value;
                if (_mana <= 0)
                {
                    // trigger chronoshift
                    GameManagerN.Instance.PunChangeState(GameManagerN.Instance._state +1);
                }
            }
        }
        public void GenerateLight()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Light.shapePath[0] = new Vector3(18, -1);
                Light.shapePath[1] = new Vector3(23, 8);
                Light.shapePath[2] = new Vector3(10, 8.3f);
                currentTile = Grid.Instance.Tiles.Last().Key;
                _playerInstance = PhotonNetwork.Instantiate("Photon/Player", Grid.Instance.Tiles.Last().Value.transform.position, Quaternion.identity);
                _playerInstance.name = "P2";
                PlayerScript = _playerInstance.GetComponent<Character1>();
                PlayerScript.ID = 2;

            }
            else
            {
                currentTile = Grid.Instance.Tiles.First().Key;
                _playerInstance = PhotonNetwork.Instantiate("Photon/Player", Grid.Instance.Tiles.First().Value.transform.position, Quaternion.identity);
                _playerInstance.name = "P1";
                PlayerScript = _playerInstance.GetComponent<Character1>();
                PlayerScript.ID = 1;
            }
            PlayerView = _playerInstance.GetComponent<PhotonView>();
            mode = Mode.Blocked;
            mana = manamax;
            
            GlobalLight.SetActive(true);
            Light.gameObject.SetActive(true);
            _playerInstance.tag = Constants.TAG_PLAYER;
            ElementsController.Instance.ChangeElement((int)GameManagerN.Instance.StarterElement);
            if (PhotonNetwork.IsMasterClient)
            {
                GameManagerN.Instance.PunChangeState(GameStateN.Player1Turn);
            }
        }
        
        public void PreparePlayerControllerForRewind()
        {
            mode = Mode.Blocked;
        }
        void DisplayRange()
        {
            
            switch (mode)
            {
                case Mode.Blocked:
                    ResetShowedTile();
                    break;
                case Mode.Move:
                    GetTileInRange(moveRange);
                    ShowTileInRange();
                    break;
                case Mode.Casting:
                    GetTileInRange(CastRange);
                    ShowTileInRange();
                    break;
            }
        }

        void GetTileInRange(float range)
        {
            Collider2D[] hell = Physics2D.OverlapCircleAll(Grid.Instance.Tiles[currentTile].transform.position, range, tileLayer);
            foreach (Collider2D col in hell)
            {
                InRangeTile.Add(col.gameObject.GetComponent<Tile>().TileId, col.gameObject.GetComponent<Tile>());
            }
        }

        void ShowTileInRange()
        {
            foreach (Tile tile in InRangeTile.Values)
            {
                tile._highlight.SetActive(true);
                tile.InRange = true;
                switch (mode)
                {
                    case Mode.Blocked:
                        break;
                    case Mode.Move:
                        tile._highlight.GetComponent<SpriteRenderer>().color = tile.MColor;
                        break;
                    case Mode.Casting:
                        tile._highlight.GetComponent<SpriteRenderer>().color = tile.CColor;
                        break;
                }
            }
        }

        void ResetShowedTile()
        {
            foreach (Tile tile in InRangeTile.Values)
            {
                tile._highlight.GetComponent<SpriteRenderer>().color = tile.NColor;
                tile.InRange = false;
                tile._highlight.SetActive(false);
            }
            InRangeTile.Clear();
        }

        public void MovePlayer(int tileID)
        {
            if (!PlayerView.IsMine) return;
            currentTile = tileID;
            _playerInstance.transform.position = Grid.Instance.Tiles[tileID].transform.position;
        }
    }
}
