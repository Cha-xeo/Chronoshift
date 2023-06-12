using Chronoshift.Tiles;
using Grid = Chronoshift.Tiles.Grid;
using UnityEngine;
using Photon.Pun;

namespace Chronoshift.Spells
{
    public class Wind1 : Spells
    {
        public override void Use()
        {
            base.Use();
            //GridManager.Instance.ChangeTileAt(ChargedElement.Instance.lastPos, _obstacleDefault);
            //GridManager.Instance.GetTileAtPos(ChargedElement.Instance.lastPos).SetUnit(flag);
            //UnitManager.Instance.SpawnUnitAt(flag, ChargedElement.Instance.lastPos);
            // ChronoManager.Instance.spellHistoryManager.Add(new ChronoManager.SpellHistoryManager(ChargedElement.Instance.lastPos, this));

        }
        public override void ChronoUse(int tileID)
        {
            base.Use();
            Grid.Instance.SetTileLocal(tileID, Element.Elements);

        }
    }
}