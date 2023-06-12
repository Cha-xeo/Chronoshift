using Photon.Pun;
using UnityEngine;
using Grid = Chronoshift.Tiles.Grid;

namespace Chronoshift.Spells
{
    public class Earth1 : Spells
    {
        public override void Use(int tileID)
        {
            base.Use(tileID);
        }
        public override void ChronoUse(int tileID)
        {
            base.ChronoUse(tileID);
            Grid.Instance.SetTileLocal(tileID, Element.Elements);
        }
    }
}
