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
        _tmp = Chronoshift.Audio.SoundManagerv2.Instance.GetEffectsVolume() + volume;
        if (_tmp <= 0 || _tmp >= 1) return;
        Chronoshift.Audio.SoundManagerv2.Instance.ChangeEffectsVolume(_tmp);
    }
    public void MuteSoundVolume()
    {
        Chronoshift.Audio.SoundManagerv2.Instance.ChangeEffectsVolume(0);
    }

    //Music volume
    public void MusicVolume(float volume)
    {
        _tmp = Chronoshift.Audio.SoundManagerv2.Instance.GetMusicVolume() + volume;
        if (_tmp <= 0 || _tmp >= 1) return;
        Chronoshift.Audio.SoundManagerv2.Instance.ChangeMusicVolume(_tmp);
    }
    public void MuteMusicVolume()
    {
        Chronoshift.Audio.SoundManagerv2.Instance.ChangeMusicVolume(0);
    }

    //Master volume
    public void MasterVolume(float volume)
    {
        _tmp = Chronoshift.Audio.SoundManagerv2.Instance.GetMasterVolume() + volume;
        if (_tmp <= 0 || _tmp >= 1) return;
        Chronoshift.Audio.SoundManagerv2.Instance.ChangeMasterVolume(_tmp);
    }
    public void MuteMasterVolume()
    {
        Chronoshift.Audio.SoundManagerv2.Instance.ChangeMasterVolume(0);
    }

    public void MenuClick()
    {
        Chronoshift.Audio.SoundManagerv2.Instance.PlaySound(_clickSoundEffect);
    }
}
