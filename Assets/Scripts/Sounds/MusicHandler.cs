using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicType
{
    StartRoom,
    LevelOne,
    LevelTwo,
    LevelThree,
    LevelFour,
}

[RequireComponent(typeof(MusicTemplates))]
public class MusicHandler : MonoBehaviour
{
    private MusicTemplates _musicTemplates;

    private void Awake()
    {
       _musicTemplates = GetComponent<MusicTemplates>();
    }

    public void PlayMusic(MusicType type)
    {
        var clip = _musicTemplates.GetMusic(type);
        
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlayMusic(clip);
    }

    public void PlayMusic(int level)
    {
        switch(level)
        {
            case 1:
                PlayMusic(MusicType.LevelOne);
                break;
            case 2:
                PlayMusic(MusicType.LevelTwo);
                break;
            case 3:
                PlayMusic(MusicType.LevelThree);
                break;
            case 4:
                PlayMusic(MusicType.LevelFour);
                break;
            default:
                return;
        }
    }
}
