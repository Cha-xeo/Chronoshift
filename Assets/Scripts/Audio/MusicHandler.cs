using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chronoshift.Gameplay.Audio
{
    public enum MusicType
    {
        MainMenu,
        InGame1,
        InGame2,
        Victory,
        Defeat,
    }

    [System.Serializable]
    public class MusicTemplate
    {
        public MusicType Type;
        public AudioClip Music;
    }
    public class MusicHandler : MonoBehaviour
    {
        [SerializeField] List<MusicTemplate> _musicTemplates;

        public void PlayMusic(int type)
        {
            var clip = GetMusic((MusicType)type);
            Chronoshift.Audio.SoundManagerv2.Instance.StopMusic();
            Chronoshift.Audio.SoundManagerv2.Instance.PlayMusic(clip);
        }
        public AudioClip GetMusic(MusicType type)
        {
            return _musicTemplates.FirstOrDefault(music => music.Type == type).Music;
        }
    }
}
