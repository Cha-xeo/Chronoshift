using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant1 : Spells
{
    // plant
    [SerializeField]
    BaseChar _unit;

    public override void Use()
    {
        base.Use();
        //GridManager.Instance.ChangeTileAt(ChargedElement.Instance.lastPos, _obstacleDefault);
        //GridManager.Instance.GetTileAtPos(ChargedElement.Instance.lastPos).SetUnit(flag);
        UnitManager.Instance.SpawnUnitAt(flag, ChargedElement.Instance.lastPos);
        ChronoManager.Instance.spellHistoryManager.Add(new ChronoManager.SpellHistoryManager(ChargedElement.Instance.lastPos, this));

    }
    public override void ChronoUse(Vector2 pos)
    {
        base.Use();
        UnitManager.Instance.SpawnUnitAt(_unit, ChargedElement.Instance.lastPos);
       // GridManager.Instance.GetTileAtPos(ChargedElement.Instance.lastPos).SetUnit(_unit);

    }
}
