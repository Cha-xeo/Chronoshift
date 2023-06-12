using Chronoshift.PlayerController;
using Chronoshift.Spells;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chronoshift.Managers
{
    public class ChronoNManager : MonoBehaviour
    {
        private static ChronoNManager instance;

        public static ChronoNManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<ChronoNManager>();
                }
                return instance;
            }
        }
        public PhotonView _view;
        List<ChronoHistory> _chrono = new();
        int _rewindReday = 0;
        void AddAction(int tileID, Chronoshift.Spells.Spells spell)
        {
            _chrono.Add( new ChronoHistory(tileID, spell));
        }

        void ClearChrono()
        {
            /*foreach (var item in _chrono)
            {
                PhotonNetwork.Destroy(item.HoldingSpell);
            }*/
            _chrono.Clear();
        }
       
        public void AddToChronoshiftLocal(int tileID, Chronoshift.Spells.Spells spell)
        {
            AddAction(tileID, spell);
        }

        [PunRPC]
        void RPC_ClearChronoshift()
        {
            ClearChrono();
        }

    
        public void StartChronoshitLocal()
        {
            StartCoroutine(LocalRewind());
        }

        IEnumerator LocalRewind()
        {
            // TODO only rewind in the master
            GameManagerN.Instance.DecreaseTimer(1);
            //PlayerNController.Instance.moveRange = PlayerNController.Instance.MaxMoveRange;
            float i = 1;
            float count = _chrono.Count;
            foreach (ChronoHistory item in _chrono)
            {
                /*if (!item.Spell)
                {
                        PlayerNController.Instance.MovePlayer(item.TileID);
                }
                else
                {
                    item.Spell.ChronoUse(item.TileID);
                }
                yield return new WaitForSeconds(1);*/
                if (item.Spell == null)
                {
                    PlayerNController.Instance.MovePlayer(item.TileID);
                }
                else
                {
                    item.Spell.ChronoUse(item.TileID);
                }
                GameManagerN.Instance.DecreaseTimer(1 - i / count);
                i+=1;
                yield return new WaitForSeconds(1);
            }
             _view.RPC("RPC_SendReady", RpcTarget.MasterClient);
        }
        [PunRPC]
        void RPC_SendReady()
        {
            _rewindReday++;
            if (_rewindReday == 2)
            {
                _view.RPC("RPC_EndRewind", RpcTarget.AllViaServer);
            }
        }

        [PunRPC]
        void RPC_EndRewind()
        {
            _rewindReday = 0;
            PlayerNController.Instance.mana = PlayerNController.Instance.manamax;
            SpellsNetwork.Instance.RewindEnd();
            ClearChrono();
            //GameManagerN.Instance.PunChangeState(GameStateN.Player1Turn);
            GameManagerN.Instance.ChangeState(GameStateN.Player1Turn);
        }
    }

    public class ChronoHistory
    {
        public int TileID;
        public int PlayerID;

        /// <summary>
        /// Local Chronoshift
        /// </summary>
        public Chronoshift.Spells.Spells Spell;

        /// <summary>
        /// Local Chronoshift
        /// </summary>
        public ChronoHistory(int tileID, Chronoshift.Spells.Spells spell)
        {
            TileID = tileID;
            if (spell == null)
            {
                Spell = null;
                return;
            }
            Spell = spell;
        }
    }
}
