using System;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Networking.Transport;
using UnityEngine.UI;

namespace Chronoshift.Multiplayer
{
    /// <summary>
    /// A simple sample showing how to use the Relay Allocation package. As the host, you can authenticate, request a relay allocation, get a join code and join the allocation.
    /// </summary>
    /// <remarks>
    /// The sample is limited to calling the Relay Allocation Service and does not cover connecting the host game client to the relay using Unity Transport Protocol.
    /// This will cause the allocation to be reclaimed about 10 seconds after creating it.
    /// </remarks>
    public class Relay : MonoBehaviour
    {
        /// <summary>
        /// The textbox displaying the Player Id.
        /// </summary>
        public Button logInBtn;
        public Image LogInImg;

        /// <summary>
        /// The dropdown displaying the region.
        /// </summary>
        public TMP_Dropdown RegionsDropdown;

        /// <summary>
        /// The textbox displaying the Allocation Id.
        /// </summary>

        /// <summary>
        /// The textbox displaying the Join Code.
        /// </summary>
        public TMP_Text JoinCodeText;

        /// <summary>
        /// The textbox displaying the Allocation Id of the joined allocation.
        /// </summary>
        public TMP_Text PlayerAllocationIdText;

        Guid hostAllocationId;
        Guid playerAllocationId;
        string allocationRegion = "";
        string joinCode = "n/a";
        bool _isHost;

        public void Nameing(string name)
        {
            if (name.Length >= 3)
            {
                logInBtn.interactable = true;
                LogInImg.CrossFadeAlpha(100, 1, true);
            }
            else
            {
                logInBtn.interactable = false;
                LogInImg.CrossFadeAlpha(2, 1, true);
            }
        }


        /// <summary>
        /// Event handler for when the Allocate button is clicked.
        /// </summary>
        public async void OnAllocate()
        {
            Debug.Log("Host - Creating an allocation.");


            // Important: Once the allocation is created, you have ten seconds to BIND
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(4);
            hostAllocationId = allocation.AllocationId;
            allocationRegion = allocation.Region;
            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            Debug.Log($"Host Allocation ID: {hostAllocationId}, default region: {allocationRegion}");

            NetworkManager.Singleton.StartHost();
            OnJoinCode();
        }

        /// <summary>
        /// Event handler for when the Get Join Code button is clicked.
        /// </summary>
        public async void OnJoinCode()
        {
            Debug.Log("Host - Getting a join code for my allocation. I would share that join code with the other players so they can join my session.");

            try
            {
                joinCode = await RelayService.Instance.GetJoinCodeAsync(hostAllocationId);
                Debug.Log("Host - Got join code: " + joinCode);
            }
            catch (RelayServiceException ex)
            {
                Debug.LogError(ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// Event handler for when the Join button is clicked.
        /// </summary>
        public async void OnJoin()
        {
            Debug.Log("Player - Joining host allocation using join code.");

            try
            {
                var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
                playerAllocationId = joinAllocation.AllocationId;
                Debug.Log("Player Allocation ID: " + playerAllocationId);
                RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
                Debug.Log(NetworkManager.Singleton.ConnectedClients.ToString());
                NetworkManager.Singleton.StartClient();
            }
            catch (RelayServiceException ex)
            {
                Debug.LogError(ex.Message + "\n" + ex.StackTrace);
            }

        }
        /// <summary>
        /// Event handler for when the Quick Join button is clicked.
        /// </summary>
        public void OnQuitRelay()
        {
            Debug.Log("Todo quit relay.");
        }

        public void Onplay()
        {
            if (_isHost) { NetworkManager.Singleton.StartHost(); return; }
            NetworkManager.Singleton.StartClient();
        }
    }
}