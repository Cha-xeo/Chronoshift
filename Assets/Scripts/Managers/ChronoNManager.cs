using Chronoshift.PlayerController;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        void AddAction(int tileID, Mode mode)
        {
            _chrono.Add( new ChronoHistory(tileID, mode));
        }

        void ClearChrono()
        {
            _chrono.Clear();
        }

        [PunRPC]
        void RPC_AddToChronoshift(int tileID, Mode mode)
        {
            AddAction(tileID, mode);
        }
        [PunRPC]
        void RPC_ClearChronoshift(int tileID, Mode mode)
        {
            ClearChrono();
        }
    }

    public class ChronoHistory
    {
        public int TileID;
        public Mode Mode;

        public ChronoHistory(int tileID, Mode mode)
        {
            TileID = tileID;
            Mode = mode;
        }
    }
}
