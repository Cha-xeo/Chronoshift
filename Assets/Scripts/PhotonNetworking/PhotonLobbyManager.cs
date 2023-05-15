using System;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

namespace Chronoshift.PhotonNetworking
{
    public class PhotonLobbyManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] TMP_InputField _createInputField;
        [SerializeField] TMP_InputField _joinInputField;
        RoomOptions roomOptions = new RoomOptions();
        private void Awake()
        {
            roomOptions.MaxPlayers = 2;
        }
        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(_createInputField.text, roomOptions);
        }
        public void JoinRoom()
        {
            //Room aled = PhotonNetwork.CurrentRoom;

            PhotonNetwork.JoinRoom(_joinInputField.text);
        }

        public void QuickJoin()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinedRoom()
        {
            // subscribe to full lobby
            // when full load level
            PhotonNetwork.LoadLevel("PhotonWaitingRoomScene");
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            CreateRoom();
        }
    }
}
