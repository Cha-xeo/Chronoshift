using Chronoshift.AplicationController;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chronoshift.UI
{
    public class UIQuitPanel : MonoBehaviour
    {
        public void Quit()
        {
            switch (AplicationController.AplicationController.Instance.m_QuitMode)
            {
                case QuitMode.ReturnToMenu:
                    Audio.SoundManagerV2.Instance.StopAllSounds();
#if !UNITY_EDITOR
			        Application.Quit();
#endif

#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                    break;
                case QuitMode.QuitGame:
                    Audio.SoundManagerV2.Instance.StopAllSounds();
#if !UNITY_EDITOR
			        Application.Quit();
#endif

#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif

                    break;
                case QuitMode.QuitApplication:
#if !UNITY_EDITOR
			        Application.Quit();
#endif

#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            gameObject.SetActive(false);
        }
    }
}
