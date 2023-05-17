using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool _isWalkable;
    private int mpsBefore;
    public float ntmx;
    public float ntmy;

    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;

    public virtual void Init(int x, int y) {

    }

    void OnMouseEnter() {
        _highlight.SetActive(true);
        ChargedElement.Instance.lastPos.Set(ntmx, ntmy);
    }
    void OnMouseExit() {
        _highlight.SetActive(false);
    }

    void OnMouseDown() {
        if(GameManager.Instance._state != GameState.CharTurn && GameManager.Instance._state != GameState.EnemyTurn)
            return;
        if (OccupiedUnit != null) {
            if (OccupiedUnit.Faction == Faction.Character) {
                UnitManager.Instance.SetSelectedChar((BaseChar)OccupiedUnit);
            } else if (OccupiedUnit.Faction == Faction.Enemy) {
                UnitManager.Instance.SetSelectedEnemy((BaseEnemy)OccupiedUnit);
            }
            else {
                if (UnitManager.Instance.SelectedChar != null || UnitManager.Instance.SelectedEnemy != null) {
                    return;
                }
            }
        }
        else {
            if (ChargedElement.Instance.holding) return;
            if (UnitManager.Instance.SelectedChar != null) {
                Debug.Log("Ally selected.");
                SetUnit(UnitManager.Instance.SelectedChar);
                UnitManager.Instance.SelectedChar.MPs -= 1;
                if (UnitManager.Instance.SelectedChar.MPs <= 0) {
                    UnitManager.Instance.SelectedChar.MPs = 4; // PAS FLEXIBLE
                    ChronoManager.Instance.Chronoshift(UnitManager.Instance.SelectedChar.transform.position, UnitManager.Instance.SelectedChar);
                    UnitManager.Instance.SetSelectedChar(null);
                    GameManager.Instance.ChangeState(GameState.Chronoshift);
                }
            }
            if (UnitManager.Instance.SelectedEnemy != null) {
                Debug.Log("Enemy selected.");
                SetUnit(UnitManager.Instance.SelectedEnemy);
                UnitManager.Instance.SelectedEnemy.MPs -= 1;
                if (UnitManager.Instance.SelectedEnemy.MPs <= 0) {
                    UnitManager.Instance.SelectedEnemy.MPs = 4; // PAS FLEXIBLE
                    ChronoManager.Instance.Chronoshift(UnitManager.Instance.SelectedEnemy.transform.position, UnitManager.Instance.SelectedEnemy);
                    UnitManager.Instance.SetSelectedEnemy(null);
                    GameManager.Instance.ChangeState(GameState.Chronoshift);
                }
            }
        }
    }

    public void SetUnit(BaseUnit unit) {
        if (unit.OccupiedTile != null)
            unit.OccupiedTile.OccupiedUnit = null;
        ChronoManager.Instance.Chronoshift(unit.transform.position, unit);
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}
