using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider Slider;
    public void SetMasterVolume()
    {
        SoundManager.Instance.ChangeMasterVolume(Slider.value);
    }

    public void SetMusicVolume()
    {
        SoundManager.Instance.ChangeMusicVolume(Slider.value);
    }
    public void SetEffectVolume()
    {
        SoundManager.Instance.ChangeEffectsVolume(Slider.value);
    }
}
