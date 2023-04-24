using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    [SerializeField] AudioClip _clickSoundEffect;
    [SerializeField] GameObject _optionCanvas;
    float _tmp;

    // Sounds volume
    public void SoundVolume(float volume)
    {
        _tmp = SoundManager.Instance.GetEffectsVolume() + volume;
        if (_tmp <= 0 || _tmp >= 1) return;
        SoundManager.Instance.ChangeEffectsVolume(_tmp);
    }
    public void MuteSoundVolume()
    {
        SoundManager.Instance.ChangeEffectsVolume(0);
    }

    //Music volume
    public void MusicVolume(float volume)
    {
        _tmp = SoundManager.Instance.GetMusicVolume() + volume;
        if (_tmp <= 0 || _tmp >= 1) return;
        SoundManager.Instance.ChangeMusicVolume(_tmp);
    }
    public void MuteMusicVolume()
    {
        SoundManager.Instance.ChangeMusicVolume(0);
    }

    //Master volume
    public void MasterVolume(float volume)
    {
        _tmp = SoundManager.Instance.GetMasterVolume() + volume;
        if (_tmp <= 0 || _tmp >= 1) return;
        SoundManager.Instance.ChangeMasterVolume(_tmp);
    }
    public void MuteMasterVolume()
    {
        SoundManager.Instance.ChangeMasterVolume(0);
    }

    public void MenuClick()
    {
        SoundManager.Instance.PlaySound(_clickSoundEffect);
    }
}
