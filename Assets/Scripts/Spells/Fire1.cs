using Chronoshift.PlayerController;
using Grid = Chronoshift.Tiles.Grid;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Chronoshift.Spells;
using Photon.Pun;

namespace Chronoshift.Spells
{
    public class Fire1 : Spells
    {
        [SerializeField] GameObject _torchePrefab;
        Light2D Torch;
        public int maxTurn = 3;
        public int turn = 0;
        // 
        public override void Use()
        {
            base.Use();
            //GridManager.Instance.ChangeTileAt(ChargedElement.Instance.lastPos, _obstacleDefault);
            //GridManager.Instance.GetTileAtPos(ChargedElement.Instance.lastPos).SetUnit(flag);
            //UnitManager.Instance.SpawnUnitAt(flag, ChargedElement.Instance.lastPos);
            //ChronoManager.Instance.spellHistoryManager.Add(new ChronoManager.SpellHistoryManager(GridManager.Instance.GetTileAtPos(ChargedElement.Instance.lastPos).transform.position, this));
            //ChronoManager.Instance.spellHistoryManager.Add(new ChronoManager.SpellHistoryManager(ChargedElement.Instance.lastPos, this));

        }
        public override void ChronoUse(int tileID)
        {
            base.ChronoUse(tileID);
            //_torchInstance = Instantiate(this, pos, Quaternion.identity);
            SpellsNetwork.Instance.TorchList.Add(Instantiate(_torchePrefab, Grid.Instance.Tiles[tileID].transform.position, Quaternion.identity));
        }

        public bool Turn()
        {
            turn++;
            return turn >= maxTurn;
        }

        [PunRPC]
        protected override void RPC_OccupieTile(int tileID)
        {
            base.RPC_OccupieTile(tileID);
        }
        [PunRPC]
        protected override void RPC_UnoOccupieTile(int tileID)
        {
            base.RPC_UnoOccupieTile(tileID);
        }
    }
}