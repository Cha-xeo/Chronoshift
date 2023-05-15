using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
namespace Chronoshift.PhotonNetworking
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        void Start ()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }
        public override void OnJoinedLobby()
        {
            SceneManager.LoadScene("PhotonLobbyScene");
        }
    }
}
