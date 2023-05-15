using Chronoshift.AplicationController;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState _state;
    bool ntm = false;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake() {
        Instance = this;
    }

    private void Start ()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ChangeState(GameState.GenerateGrid);
        }
        else
        {
            ChangeState(GameState.SpawnChar);
        }
    }

    public void ChangeState(GameState newState) {
        _state = newState;
        Debug.Log("new state: "+newState);
        switch(newState) 
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnChar:
                UnitManager.Instance.SpawnCharacter();
                break;
            case GameState.SpawnEnemy:
                UnitManager.Instance.SpawnEnemies();
                break;
            case GameState.CharTurn:
                break;
            case GameState.EnemyTurn:
                break;
            case GameState.Chronoshift:
                ChronoManager.Instance.Rewind();
                break;
            case GameState.Rewind:
                break;
            default:
                break;
                // throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        // OnGameStateChanged?.Invoke(newState);
    }

}
    public enum GameState {
        GenerateGrid = 0,
        SpawnChar = 1,
        SpawnEnemy = 2,
        CharTurn = 3,
        EnemyTurn = 4,
        Chronoshift = 5,
        Rewind = 6
    }
