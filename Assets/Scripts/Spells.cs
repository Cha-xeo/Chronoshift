using Chronoshift.PlayerController;
using UnityEngine;
using Chronoshift.Tiles;
using Grid = Chronoshift.Tiles.Grid;
using Photon.Pun;
using Chronoshift.Managers;

namespace Chronoshift.Spells
{
    public abstract class Spells : MonoBehaviour, ISpells
    {
        public ElemScriptable Element;
        public BaseChar flag;
        GameObject _flagInstance;
        [SerializeField] protected Tile _spellTile;
        [SerializeField] PhotonView SpellView;
        public virtual void Use()
        {
            //TODO fuck around
            if (!PlayerNController.Instance.PlayerView.IsMine) return;
            SpellsNetwork.Instance.OccupieTile(ChargedElement.Instance.LastTileID, Element.Elements);
            //if (!PlayerNController.Instance.PlayerView.IsMine) return;
            //SpellView.RPC("RPC_OccupieTile", RpcTarget.AllViaServer, ChargedElement.Instance.LastTileID);
        }
        public virtual void ChronoUse(int tileID)
        {
            if (!PlayerNController.Instance.PlayerView.IsMine) return;
            SpellsNetwork.Instance.UnoOccupieTile(tileID);
            //Destroy(_flagInstance);
        }
    }
}