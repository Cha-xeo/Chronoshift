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
    public class MusicHandlerV2 : MonoBehaviour
    {
        [SerializeField] List<MusicTemplate> _musicTemplates;

        public void PlayMusic(int type)
        {
            var clip = GetMusic((MusicType)type);
            Chronoshift.Audio.SoundManagerV2.Instance.StopMusic();
            Chronoshift.Audio.SoundManagerV2.Instance.PlayMusic(clip);
        }
        public void PlayMusic(MusicType type)
        {
            var clip = GetMusic(type);
            Chronoshift.Audio.SoundManagerV2.Instance.StopMusic();
            Chronoshift.Audio.SoundManagerV2.Instance.PlayMusic(clip);
        }
        private AudioClip GetMusic(MusicType type)
        {
            return _musicTemplates.FirstOrDefault(music => music.Type == type).Music;
        }
    }
}
