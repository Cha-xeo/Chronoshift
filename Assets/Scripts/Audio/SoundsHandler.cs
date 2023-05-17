using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chronoshift.Audio
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
        public MusicType musicType;
        public AudioClip Music;
    }
    [System.Serializable]
    public class SoundTemplate
    {
        public string name;
        public AudioClip[] Effect;
    }

    public class SoundsHandler : MonoBehaviour
    {
        [SerializeField] List<MusicTemplate> _musicTemplates;
        [SerializeField] List<SoundTemplate> _effectTemplate;


        public int EffectContain(string _name)
        {
            int length = _effectTemplate.Count;
            for (int i = 0; i < length; i++)
            {
                if (_effectTemplate[i].name.Contains(_name)) return i;
            }
            return 1;
            /*foreach (SoundTemplate template in _effectTemplate)
            {
                if (template.name.Contains(_name)) return true;
            }
            return false;*/
        }    

        public void PlayMusic(int type)
        {
            var clip = GetMusic((MusicType)type);
            SoundManagerV2.Instance.StopMusic();
            SoundManagerV2.Instance.PlayMusic(clip);
        }
        public AudioClip GetMusic(MusicType type)
        {
            return _musicTemplates.FirstOrDefault(music => music.musicType == type).Music;
        }
    }
}
