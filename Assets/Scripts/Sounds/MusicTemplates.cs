using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class MusicTemplate
{
    public MusicType Type;
    public AudioClip Music;
}

public class MusicTemplates : MonoBehaviour
{
    [SerializeField]
    private List<MusicTemplate> _musicTemplates;
    
    public AudioClip GetMusic(MusicType type)
    {
        return _musicTemplates.FirstOrDefault(music => music.Type == type).Music;
    }
}
