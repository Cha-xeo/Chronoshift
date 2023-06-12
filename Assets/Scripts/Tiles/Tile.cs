using Chronoshift.Managers;
using Chronoshift.PlayerController;
using Photon.Pun;
using UnityEngine;


namespace Chronoshift.Tiles
{
    public abstract class Tile : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer _renderer;
        public Color NColor, CColor, MColor;
        public GameObject _highlight;
        //[SerializeField] PhotonView _view;
        [SerializeField] private bool _isWalkable;
        private int mpsBefore;
        public int layer;
        public int TileId;
        public bool InRange;
        
        public Constants.Elements Elements;
        public BaseUnit OccupiedUnit;
        public bool Walkable => _isWalkable && OccupiedUnit == null;
        bool _showUnit;

        public virtual void Init(int id, int slayer)
        {
            name = $"Tile {id}";
            TileId = id;
            layer = slayer;
            GetComponent<SpriteRenderer>().sortingOrder = layer;
            _highlight.GetComponent<SpriteRenderer>().sortingOrder = slayer + 1;
            transform.parent = GridGenerator.Instance.World;
        }

        void OnMouseEnter()
        {
            if (!PlayerNController.Instance.PlayerView.IsMine) return;
            if (InRange)
            {
                _highlight.SetActive(false);
            }
            else
            {
                _highlight.SetActive(true);
            }
            ChargedElement.Instance.LastTileID = TileId;
        }
        void OnMouseExit()
        {
            if (!PlayerNController.Instance.PlayerView.IsMine) return;
            if (InRange)
            {
                _highlight.SetActive(true);
            }
            else
            {
                _highlight.SetActive(false);
            }
        }

        /*void OnMouseDown()
        {
            if (GameManager.Instance._state != GameState.CharTurn && GameManager.Instance._state != GameState.EnemyTurn)
                return;
            if (OccupiedUnit != null)
            {
                if (OccupiedUnit.Faction == Faction.Character)
                {
                    UnitManager.Instance.SetSelectedChar((BaseChar)OccupiedUnit);
                }
                else if (OccupiedUnit.Faction == Faction.Enemy)
                {
                    UnitManager.Instance.SetSelectedEnemy((BaseEnemy)OccupiedUnit);
                }
                else
                {
                    if (UnitManager.Instance.SelectedChar != null || UnitManager.Instance.SelectedEnemy != null)
                    {
                        return;
                    }
                }
            }
            else
            {
                if (ChargedElement.Instance.holding) return;
                if (UnitManager.Instance.SelectedChar != null)
                {
                    Debug.Log("Ally selected.");
                    SetUnit(UnitManager.Instance.SelectedChar);
                    UnitManager.Instance.SelectedChar.MPs -= 1;
                    if (UnitManager.Instance.SelectedChar.MPs <= 0)
                    {
                        UnitManager.Instance.SelectedChar.MPs = 4; // PAS FLEXIBLE
                        ChronoManager.Instance.Chronoshift(UnitManager.Instance.SelectedChar.transform.position, UnitManager.Instance.SelectedChar);
                        UnitManager.Instance.SetSelectedChar(null);
                        GameManager.Instance.ChangeState(GameState.Chronoshift);
                    }
                }
                if (UnitManager.Instance.SelectedEnemy != null)
                {
                    Debug.Log("Enemy selected.");
                    SetUnit(UnitManager.Instance.SelectedEnemy);
                    UnitManager.Instance.SelectedEnemy.MPs -= 1;
                    if (UnitManager.Instance.SelectedEnemy.MPs <= 0)
                    {
                        UnitManager.Instance.SelectedEnemy.MPs = 4; // PAS FLEXIBLE
                        ChronoManager.Instance.Chronoshift(UnitManager.Instance.SelectedEnemy.transform.position, UnitManager.Instance.SelectedEnemy);
                        UnitManager.Instance.SetSelectedEnemy(null);
                        GameManager.Instance.ChangeState(GameState.Chronoshift);
                    }
                }
            }
        }*/
        void OnMouseDown()
        {
            if (!PlayerNController.Instance.PlayerView.IsMine) return;

            if (!PlayerNController.Instance.IsPlaying || PlayerNController.Instance.mode == Mode.Blocked || GameManagerN.Instance._state == GameStateN.GenerateGrid || GameManagerN.Instance._state == GameStateN.Chronoshift || !InRange || !ChargedElement.Instance.canCast) return;//|| !ChargedElement.Instance.canCast

            switch (PlayerNController.Instance.mode)
            {
                case Mode.Move:
                    PlayerNController.Instance.MovePlayer(TileId);
                    /// Test local chronoshift
                    ChronoNManager.Instance.AddToChronoshiftLocal(TileId, null);
                    /// ChronoNManager.Instance._view.RPC("RPC_AddToChronoshift", RpcTarget.AllViaServer, TileId, "", PlayerNController.Instance.PlayerScript.ID);
                    break;
                case Mode.Casting:
                    /// Test local chronoshift
                    ChargedElement.Instance.HoldingSpell.GetComponent<Chronoshift.Spells.Spells>().Use();
                    ChronoNManager.Instance.AddToChronoshiftLocal(TileId, ChargedElement.Instance.HoldingSpell.GetComponent<Chronoshift.Spells.Spells>());
                    /// int indexOfParenthesis = ChargedElement.Instance.HoldingSpell.name.IndexOf("(");
                    /// ChronoNManager.Instance._view.RPC("RPC_AddToChronoshift", RpcTarget.AllViaServer, TileId, ChargedElement.Instance.HoldingSpell.name.Substring(0, indexOfParenthesis), PlayerNController.Instance.PlayerScript.ID);
                    ///PhotonNetwork.Destroy(ChargedElement.Instance.HoldingSpell);
                    break;
            }
            PlayerNController.Instance.mana--;
            PlayerNController.Instance.mode = Mode.Move;
        }

        public void SetUnit(BaseUnit unit)
        {
            if (unit.OccupiedTile != null)
                unit.OccupiedTile.OccupiedUnit = null;
            ChronoManager.Instance.Chronoshift(unit.transform.position, unit);
            unit.transform.position = transform.position;
            OccupiedUnit = unit;
            unit.OccupiedTile = this;
        }

        public void RevealTile()
        {
            _showUnit = true;
        }

        public void HideTile()
        {
            _showUnit = false;
        }
    }
}