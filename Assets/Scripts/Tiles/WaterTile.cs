using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : Tile
{

    // when the player walks on he lose more moving points

    // private Animation anim;

    bool roundEnd = false;
    int health = 3;

    void Update() {
        if (roundEnd) {
            TakeDamages();
        }
    }

    void TakeDamages() {
        health--;
        if (health == 0) {
            // anim.Play("destroyed");
            // Destroy(GameObject);
        }
    }
}