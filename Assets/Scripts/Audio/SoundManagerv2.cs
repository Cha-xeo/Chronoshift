using Chronoshift.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chronoshift.Audio
{
    public class SoundManagerv2 : MonoBehaviour
    {
        public static SoundManagerv2 Instance { get; private set; }

        [SerializeField]
        AudioSource _musicSource, _effectsSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                _effectsSource.volume = ClientPrefs.GetEffectVolume();
                _musicSource.volume = ClientPrefs.GetMusicVolume();
                AudioListener.volume = ClientPrefs.GetMasterVolume();
            }
            else
                Destroy(gameObject);
        }

        public void PlaySound(AudioClip clip)
        {
            _effectsSource.PlayOneShot(clip);
        }

        public void PlayMusic(AudioClip clip)
        {
            _musicSource.clip = clip;
            _musicSource.Play();
        }

        public void ContinueMusic()
        {
            _musicSource.Play();
        }
        public void StopMusic()
        {
            _musicSource.Stop();
        }

        public void StopEffectsSound()
        {
            _effectsSource.Stop();
        }

        public void StopAllSounds()
        {
            _musicSource.Stop();
            _effectsSource.Stop();
        }

        public void ChangeMasterVolume(float value)
        {
            ClientPrefs.SetMasterVolume(value);
            AudioListener.volume = value;
        }

        public void ChangeMusicVolume(float value)
        {
            ClientPrefs.SetMusicVolume(value);
            _musicSource.volume = value;
        }

        public void ChangeEffectsVolume(float value)
        {
            ClientPrefs.SetEffectVolume(value);
            _effectsSource.volume = value;
        }

        public float GetMusicVolume()
        {
            return _musicSource.volume;
        }
        public float GetEffectsVolume()
        {
            return _effectsSource.volume;
        }
        public float GetMasterVolume()
        {
            return AudioListener.volume;
        }
    }
}