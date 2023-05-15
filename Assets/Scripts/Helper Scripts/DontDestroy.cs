using System.Collections;
using System.Collections.Generic;
using Unity.Multiplayer.Samples.Utilities;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    //public static DontDestroy Instance { get; private set; }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    /*private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }*/
}
