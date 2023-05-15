using UnityEngine;
using UnityEngine.UI;

namespace Chronoshift.Gameplay.Audio
{
    public class UiAudioVolume : MonoBehaviour
    {
        [SerializeField] Slider _musicSlider, _effectSlider, _masterSlider;
        private void OnEnable()
        {
            _musicSlider.value = Chronoshift.Audio.SoundManagerv2.Instance.GetMusicVolume();
            _effectSlider.value = Chronoshift.Audio.SoundManagerv2.Instance.GetEffectsVolume();
            _masterSlider.value = Chronoshift.Audio.SoundManagerv2.Instance.GetMasterVolume();
        }

        public void EffectVolume()
        {
            Chronoshift.Audio.SoundManagerv2.Instance.ChangeEffectsVolume(_effectSlider.value);
        }
        public void MusicVolume()
        {
            Chronoshift.Audio.SoundManagerv2.Instance.ChangeMusicVolume(_musicSlider.value);
        }
        public void MasterVolume()
        {
            Chronoshift.Audio.SoundManagerv2.Instance.ChangeMasterVolume(_masterSlider.value);
        }
    }
}
