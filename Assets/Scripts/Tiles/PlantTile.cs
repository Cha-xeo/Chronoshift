using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTile : MonoBehaviour
{

    // auto destroy after x round
    // attack a the end of the player turn
    // is destructable by the enemy
    // lose health point after each attack

    bool roundEnd = false;
    float range = 3f;
    int health = 3;

    void Update() {
        if (roundEnd) {
            Attack();
        }
    }

    void Attack() {
        Debug.Log("turret attack");
        health--;
    }

    void TakeDamages() {
        health--;
    }
}
