using Chronoshift.AplicationController;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Chronoshift.Gameplay.UI
{
    public class UIQuitPanel : MonoBehaviour
    {
        [SerializeField]
        QuitMode m_QuitMode = QuitMode.ReturnToMenu;


        public void Quit()
        {
            switch (m_QuitMode)
            {
                case QuitMode.ReturnToMenu:
                    SceneManager.LoadScene("PhotonLobbyScene");
                    break;
                case QuitMode.QuitApplication:
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            gameObject.SetActive(false);
        }
    }
}