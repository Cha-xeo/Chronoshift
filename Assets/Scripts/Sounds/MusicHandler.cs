using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicType
{
    MainMenu,
    InGame1,
    InGame2,
    Victory,
    Defeat,
}

[RequireComponent(typeof(MusicTemplates))]
public class MusicHandler : MonoBehaviour
{
    [SerializeField] MusicTemplates _musicTemplates;
    public static MusicHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayMusic(int type)
    {
        var clip = _musicTemplates.GetMusic((MusicType)type);
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlayMusic(clip);
    }
}
