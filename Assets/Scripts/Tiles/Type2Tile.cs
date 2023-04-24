using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type2Tile : Tile
{
    [SerializeField] private Color _baseColor, _offsetColor, _specialColor;

    public override void Init(int x, int y) {
        // var offsetColor = (x%2 == 0 && y%2 != 0) || (x%2 != 0 && y%2 == 0);
        var isOffset = (x+y)%2 == 1;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }
}
