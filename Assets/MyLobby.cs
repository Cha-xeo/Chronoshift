using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class MyLobby : MonoBehaviour
{
    Lobby _host;
    float hearbeat;
    async void Start()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () => {
            Debug.Log("Signed in"+ AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void Update()
    {
        // Keep the lobby alive for more than 30s
        // HandleHearthbeat();
    }

    private async void HandleHearthbeat()
    {
        if (_host != null)
        {
            hearbeat -= Time.deltaTime;
            if (hearbeat < 0f)
            {
                hearbeat = 15f;
                await LobbyService.Instance.SendHeartbeatPingAsync(_host.Id);
            }
        }
    }
    public async void CreateLobby(string LobbyName)
    {
        try
        {
            int maxPlayers = 2;
            Lobby _lobby = await LobbyService.Instance.CreateLobbyAsync(LobbyName, maxPlayers);
            _host = _lobby;
            Debug.Log("Lobby: " + _lobby.Name + " " + _lobby.MaxPlayers);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void ListLobbies()
    {
        try
        {
            // Todo querry filter
            QueryResponse query = await Lobbies.Instance.QueryLobbiesAsync();

            Debug.Log("Lobbies found: " + query.Results.Count);
            foreach (Lobby result in query.Results)
            {
                Debug.Log(result.Name+" "+result.MaxPlayers);
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }

    }
    public async void JoinLobbie()
    {
        try
        {
            // Todo querry filter
            QueryResponse query = await Lobbies.Instance.QueryLobbiesAsync();

            Debug.Log("Lobbies found: " + query.Results.Count);
            foreach (Lobby result in query.Results)
            {
                Debug.Log(result.Name+" "+result.MaxPlayers);
            }

            await Lobbies.Instance.JoinLobbyByIdAsync(query.Results[0].Id);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }

    }
    public async void QuickJoin()
    {
        try
        {
            // Todo querry filter
            await LobbyService.Instance.QuickJoinLobbyAsync();
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }

    }
}


