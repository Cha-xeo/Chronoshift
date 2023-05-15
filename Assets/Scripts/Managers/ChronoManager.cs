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

    public class HistoryManager {
        public Vector3 Pos_History;
        public int Unit_Id;
        public BaseUnit Unit;

        public HistoryManager(Vector3 position, int unit_id, BaseUnit _unit) {
            Pos_History = position;
            Unit_Id = unit_id;
            Unit = _unit;
        }
    }

    public List<HistoryManager> historyManager;

    void Awake() {
        Instance = this;
        Pos_history = new List<Vector3>();
        historyManager = new List<HistoryManager>();
        grandReset = TurnsForRewind;
        Debug.Log("History creates itself.");
    }

    public void Chronoshift(Vector3 position, BaseUnit unit) {
        Debug.Log(position);
        _herald = unit;
        Pos_history.Add(position);
        historyManager.Add(new HistoryManager(position, _herald.ID, _herald));
        Debug.Log("Unit "+_herald.ID+" saved position "+position+". Positions stored: "+historyManager.Count);
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

    IEnumerator TimeLapse() { //Note : add a lag at the end of the move before rewind
        // foreach(Vector3 pos in Pos_history) {
        //     _herald.transform.position = pos;
            // yield return new WaitForSeconds(1);
        // }
        for (int cnt = 0; cnt < historyManager.Count; cnt++) {
            Debug.Log("Unit "+historyManager[cnt].Unit_Id+" saved position "+historyManager[cnt].Pos_History);
            historyManager[cnt].Unit.transform.position = historyManager[cnt].Pos_History;
            yield return new WaitForSeconds(1);
        }
    }
}
