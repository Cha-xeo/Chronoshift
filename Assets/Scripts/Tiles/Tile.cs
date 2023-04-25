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

    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;

    public virtual void Init(int x, int y) {

    }

    void OnMouseEnter() {
        _highlight.SetActive(true);
    }
    void OnMouseExit() {
        _highlight.SetActive(false);
    }

    void OnMouseDown() {
        if(GameManager.Instance._state != GameState.CharTurn)
            return;
        if (OccupiedUnit != null) {
            if (OccupiedUnit.Faction == Faction.Character) {
                UnitManager.Instance.SetSelectedChar((BaseChar)OccupiedUnit);
            }
            else {
                if (UnitManager.Instance.SelectedChar != null) {
                    return;
                }
            }
        }
        else {
            if (UnitManager.Instance.SelectedChar != null) {

                SetUnit(UnitManager.Instance.SelectedChar);
                UnitManager.Instance.SelectedChar.MPs -= 1;
                Debug.Log("Current MPs: "+UnitManager.Instance.SelectedChar.MPs);
                if (UnitManager.Instance.SelectedChar.MPs <= 0) {
                    UnitManager.Instance.SelectedChar.MPs = 4; // PAS FLEXIBLE
                    ChronoManager.Instance.Chronoshift(UnitManager.Instance.SelectedChar.transform.position, UnitManager.Instance.SelectedChar);
                    UnitManager.Instance.SetSelectedChar(null);
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
