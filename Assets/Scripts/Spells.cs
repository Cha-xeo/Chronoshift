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
            //if (!PlayerNController.Instance.PlayerView.IsMine) return;
            SpellView.RPC("RPC_OccupieTile", RpcTarget.AllViaServer, ChargedElement.Instance.LastTileID);
        }
        public virtual void ChronoUse(int tileID)
        {
            if (!PlayerNController.Instance.PlayerView.IsMine) return;
            SpellView.RPC("RPC_UnoOccupieTile", RpcTarget.AllViaServer, tileID);
            Destroy(_flagInstance);

        }

        protected virtual void RPC_OccupieTile(int tileID)
        {
            //ChronoNManager.Instance.
            //ChronoManager.Instance.spellHistoryManager.Add(new ChronoManager.SpellHistoryManager(tileID, this));
            Grid.Instance.Tiles[tileID].OccupiedUnit = flag;
            _flagInstance = PhotonNetwork.Instantiate("Photon/Flag/" + flag.gameObject.name, Grid.Instance.Tiles[tileID].transform.position, Quaternion.identity);
        }

        protected virtual void RPC_UnoOccupieTile(int tileID)
        {
            Grid.Instance.Tiles[tileID].OccupiedUnit = null;
        }
    }
}