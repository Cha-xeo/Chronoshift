using Chronoshift.PlayerController;
using UnityEngine;
using Chronoshift.Tiles;
using Photon.Pun;

namespace Chronoshift.Spells
{
    public abstract class Spells : MonoBehaviour, ISpells
    {
        public ElemScriptable Element;
        public BaseChar flag;
        [SerializeField] protected Tile _spellTile;
        [SerializeField] PhotonView SpellView;
        public virtual void Use(int tileID)
        {
            if (!PlayerNController.Instance.PlayerView.IsMine) return;
            SpellsNetwork.Instance.OccupieTile(tileID, Element.Elements);
        }
        public virtual void ChronoUse(int tileID)
        {
            if (!PlayerNController.Instance.PlayerView.IsMine) return;
            SpellsNetwork.Instance.UnoOccupieTile(tileID);
        }
    }
}