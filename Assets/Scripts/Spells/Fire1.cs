using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Fire1 : Spells
{
    Light2D Torch;

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
        Instantiate(this, (Vector2)ChargedElement.Instance.lastPos, Quaternion.identity);
    }
}
