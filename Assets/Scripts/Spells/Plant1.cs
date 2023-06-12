using Chronoshift.Spells;
using Grid = Chronoshift.Tiles.Grid;
using Photon.Pun;
using UnityEngine;

namespace Chronoshift.Spells
{
    public class Plant1 : Spells
    {
        // plant
        public int MaxTurn = 3;
        [SerializeField]
        BaseChar _unit;

        public override void Use()
        {
            base.Use();
            //GridManager.Instance.ChangeTileAt(ChargedElement.Instance.lastPos, _obstacleDefault);
            //GridManager.Instance.GetTileAtPos(ChargedElement.Instance.lastPos).SetUnit(flag);
            //UnitManager.Instance.SpawnUnitAt(flag, ChargedElement.Instance.lastPos);
            //ChronoManager.Instance.spellHistoryManager.Add(new ChronoManager.SpellHistoryManager(ChargedElement.Instance.lastPos, this));

        }
        public override void ChronoUse(int tileID)
        {
            base.Use();
            SpellsNetwork.Instance.PlantList.Add(new SpellData(PhotonNetwork.Instantiate("Photon/Plant", Grid.Instance.Tiles[tileID].transform.position, Quaternion.identity), MaxTurn));
            //UnitManager.Instance.SpawnUnitAt(_unit, pos);
            // GridManager.Instance.GetTileAtPos(ChargedElement.Instance.lastPos).SetUnit(_unit);
        }
    }
}