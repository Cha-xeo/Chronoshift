using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BaseUnit : MonoBehaviour
{
    public int ID;

    public Tile OccupiedTile;
    public Faction Faction;
    public int MPs;

    //[SerializeField] PhotonView PhotonView;
}
