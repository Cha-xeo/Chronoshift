using UnityEngine;
using UnityEngine.UI;

namespace Chronoshift.Gameplay.Audio
{
    public class UiAudioVolume : MonoBehaviour
    {
        [SerializeField] Slider _musicSlider, _effectSlider, _masterSlider;
        private void OnEnable()
        {
            _musicSlider.value = Chronoshift.Audio.SoundManagerV2.Instance.GetMusicVolume();
            _effectSlider.value = Chronoshift.Audio.SoundManagerV2.Instance.GetEffectsVolume();
            _masterSlider.value = Chronoshift.Audio.SoundManagerV2.Instance.GetMasterVolume();
        }

        public void EffectVolume()
        {
            Chronoshift.Audio.SoundManagerV2.Instance.ChangeEffectsVolume(_effectSlider.value);
        }
        public void MusicVolume()
        {
            Chronoshift.Audio.SoundManagerV2.Instance.ChangeMusicVolume(_musicSlider.value);
        }
        public void MasterVolume()
        {
            Chronoshift.Audio.SoundManagerV2.Instance.ChangeMasterVolume(_masterSlider.value);
        }
    }
}
