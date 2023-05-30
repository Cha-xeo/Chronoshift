using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronoshift.Tiles;
public class WindTile : Tile
{

    // reveal / knock up the enemy if he walks on
    // auto destroy after activation

    // private Animation anim;

    int health = 1;
    bool activated = false;
    float bumpDuration = 2;

    void Update() {
        if (activated) {
            // affect player
            // anim.Play("wind");
            // Destroy(GameObject);
        }
    }
}
