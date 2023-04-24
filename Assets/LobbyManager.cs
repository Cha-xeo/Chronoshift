using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] MyLobby _lobby;
    [SerializeField] TMP_InputField _createfield;
    [SerializeField] TMP_InputField _joinfield;

    public void CreateLobby()
    {
        _lobby.CreateLobby(_createfield.text);
    }
    public void JoinLobby()
    {
        _lobby.ListLobbies();
        //_lobby.JoinLobby(_joinfield.text);
    }
}
