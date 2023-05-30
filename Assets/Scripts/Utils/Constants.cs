using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    // public const string xxx = "";
    public const string TAG_PLAYER = "Player";
    public const string TAG_MENUSCENE = "PhotonLoadingScene";
    public const string TAG_GAMESCENE = "Game";
    public const string TAG_TILE = "Tile";
    public const string YTURN = "Your turn";
    public const string ETURN = "Ennemy turn";
    public const string RTURN = "Rewind";
    public enum Elements
    {
        None,
        Earth,
        Fire,
        Plant,
        Water,
        Wind
    }
    public enum TileElements
    {
        None,
        Earth,
        Water,
        Wind
    }
}