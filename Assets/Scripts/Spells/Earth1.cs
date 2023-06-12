using Photon.Pun;
using UnityEngine;
using Grid = Chronoshift.Tiles.Grid;

namespace Chronoshift.Spells
{
    public class Earth1 : Spells
    {
        public override void Use()
        {
            base.Use();
        }
        public override void ChronoUse(int tileID)
        {
            base.Use();
            Grid.Instance.SetTileLocal(tileID, Element.Elements);
        }
    }
}
