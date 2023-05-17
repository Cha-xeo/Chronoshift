using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind1 : Spells
{
    [SerializeField]
    Tile _windTile;
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
        GridManager.Instance.ChangeTileAt(pos, _windTile);

    }
}
