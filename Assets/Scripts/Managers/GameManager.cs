using Chronoshift;
using Chronoshift.AplicationController;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState _state;
    [SerializeField] Image _image;
    bool ntm = false;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake() {
        Instance = this;
    }

    private void Start ()
    {
        ChangeState(GameState.GenerateGrid);
        /*if (PhotonNetwork.IsMasterClient)
        {
            ChangeState(GameState.GenerateGrid);
        }
        else
        {
            ChangeState(GameState.SpawnChar);
        }*/
    }
    void DoStuff()
    {
        _image.fillAmount = 1;
        GameManager.Instance.ChangeState(GameState.Chronoshift);
        // Whatever you want to happen when the countdown finishes
    }

    IEnumerator Countdown(float seconds)
    {
        float duration = seconds;
        float totalTime = 0f;   
        while (duration > 0)
        {
            _image.fillAmount = duration / seconds;
            duration -= Time.deltaTime;
            yield return null;
        }
        // DoStuff();
    }

    public void ChangeState(GameState newState) {
        _state = newState;
        //Debug.Log("new state: "+newState);
        switch(newState) 
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnChar:
                StartCoroutine(Countdown(15));
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
        Rewind = 6,
    }
