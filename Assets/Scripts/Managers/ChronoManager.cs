using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoManager : MonoBehaviour
{
    public static ChronoManager Instance;
    private List<Vector3> Pos_history; //Double entry array with <unit : positions>
    private BaseUnit _herald;
    [SerializeField] public int TurnsForRewind;
    private int grandReset;

    void Awake() {
        Instance = this;
        Pos_history = new List<Vector3>();
        grandReset = TurnsForRewind;
        Debug.Log("History creates itself.");
    }
    
    public void Chronoshift(Vector3 position, BaseUnit unit) {
        Debug.Log(position);
        _herald = unit;
        Pos_history.Add(position);
    }

    public void Rewind() {
        if (grandReset <= 0) {
            Pos_history.RemoveAt(0);
            StartCoroutine(TimeLapse());
            Awake();
            GameManager.Instance.ChangeState(GameState.CharTurn);
        } else {
            grandReset -= 1;
            Pos_history.RemoveAt(Pos_history.Count-1); // Because the last tile is also the first of the next move
            GameManager.Instance.ChangeState(GameState.CharTurn);
        }
    }

    IEnumerator TimeLapse() {
        foreach(Vector3 pos in Pos_history) {
            // Debug.Log("History is safeguarded: " + pos);
            _herald.transform.position = pos;
            yield return new WaitForSeconds(1);
        }
    }
}
