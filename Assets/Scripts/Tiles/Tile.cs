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
        [SerializeField] public bool _isWalkable = true;
        private int mpsBefore;
        public int layer;
        public int TileId;
        public bool InRange;
        
        public Constants.Elements Elements;
        public BaseUnit OccupiedUnit;
        public bool Walkable => _isWalkable && OccupiedUnit == null;
        bool _showUnit;

        public virtual void Init(int id, int slayer, bool shallWalk)
        {
            _isWalkable = shallWalk;
            name = $"Tile {id}";
            TileId = id;
            layer = slayer;
            GetComponent<SpriteRenderer>().sortingOrder = layer;
            _highlight.GetComponent<SpriteRenderer>().sortingOrder = slayer + 1;
            transform.parent = GridGenerator.Instance.World;
            // if (_isWalkable == false) Grid.Instance.SetTileLocal(id, Constants.Elements.Earth);
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

        void OnMouseDown()
        {
            if (!PlayerNController.Instance.PlayerView.IsMine) return;

            if (!PlayerNController.Instance.IsPlaying || PlayerNController.Instance.mode == Mode.Blocked || GameManagerN.Instance._state == GameStateN.GenerateGrid || GameManagerN.Instance._state == GameStateN.Chronoshift || !InRange || !ChargedElement.Instance.canCast) return;//|| !ChargedElement.Instance.canCast

            switch (PlayerNController.Instance.mode)
            {
                case Mode.Move:
                    // if (_isWalkable != true) return;
                    PlayerNController.Instance.MovePlayer(TileId);
                    ChronoNManager.Instance.AddToChronoshiftLocal(TileId, null);
                    break;
                case Mode.Casting:
                    // if (_isWalkable != true) return;
                    ChargedElement.Instance.HoldingSpell.GetComponent<Chronoshift.Spells.Spells>().Use(TileId);
                    ChronoNManager.Instance.AddToChronoshiftLocal(TileId, ChargedElement.Instance.HoldingSpell.GetComponent<Chronoshift.Spells.Spells>());

                    break;
            }
            PlayerNController.Instance.mana--;
            GameManagerN.Instance.DecreaseTimer((float)PlayerNController.Instance.mana / (float)PlayerNController.Instance.manamax);
            PlayerNController.Instance.mode = Mode.Move;
        }

       /* public void SetUnit(BaseUnit unit)
        {
            if (unit.OccupiedTile != null)
                unit.OccupiedTile.OccupiedUnit = null;
            ChronoManager.Instance.Chronoshift(unit.transform.position, unit);
            unit.transform.position = transform.position;
            OccupiedUnit = unit;
            unit.OccupiedTile = this;
        }*/

        public void RevealTile()
        {
            _showUnit = true;
        }

        public void HideTile()
        {
            _showUnit = false;
        }

        public void FlipWalkable(bool toAdd)
        {
            _isWalkable = toAdd;
        }
    }
}