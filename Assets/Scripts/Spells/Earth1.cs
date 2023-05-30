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
