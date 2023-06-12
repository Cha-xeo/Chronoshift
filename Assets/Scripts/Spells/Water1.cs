using Chronoshift.Tiles;
using Grid = Chronoshift.Tiles.Grid;
using UnityEngine;
using Photon.Pun;

namespace Chronoshift.Spells
{
    public class Water1 : Spells
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