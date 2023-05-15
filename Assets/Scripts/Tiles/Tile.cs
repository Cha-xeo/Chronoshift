using System.Collections;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class Tile : MonoBehaviour
{
    public byte Id { get; set; }
    public static object Deserialize(byte[] data)
    {
        var result = new Tile();
        result.Id = data[0];
        return result;
    }

    public static byte[] Serialize(object customType)
    {
        var c = (Tile)customType;
        return new byte[] { c.Id };
    }
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool _isWalkable;
    private int mpsBefore;

    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;
    [SerializeField] PhotonView _view;
    public virtual void Init(int x, int y) {

    }
    void OnMouseEnter() {
        if (!_view.IsMine) return;
        _highlight.SetActive(true);
    }
    void OnMouseExit() {
        if (!_view.IsMine) return;
        _highlight.SetActive(false);
    }

    void OnMouseDown() 
    {
        if (!_view.IsMine) return;
        
        if (GameManager.Instance._state != GameState.CharTurn)
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

    public void SetUnit(BaseUnit unit) 
    {
        if (!_view.IsMine) return;
        if (unit.OccupiedTile != null)
        {
            unit.OccupiedTile.OccupiedUnit = null;
        }
        ChronoManager.Instance.Chronoshift(unit.transform.position, unit);
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}
