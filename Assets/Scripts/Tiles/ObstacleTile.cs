using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronoshift.Tiles;
public class ObstacleTile : Tile
{
    // must block the enemy
    // auto destroy after x round

    bool roundEnd = false;
    int health = 3;

    void Update() {
        if (roundEnd) {
            TakeDamages();
        }
    }

    void TakeDamages() {
        health--;
    }
}
