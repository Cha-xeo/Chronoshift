using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chronoshift.AplicationController
{
    public enum QuitMode
    {
        ReturnToMenu,
        QuitGame,
        QuitApplication
    }

    public enum StartMode
    {
        None,
        Menu,
        Game
    }
    public class AplicationController : MonoBehaviour
    {
        public static AplicationController Instance { get; private set; }
        public bool gameIsPaused;
        [SerializeField] StartMode _start;

        void Awake()
        {
            if (Instance)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
                
            }
        }

        public QuitMode m_QuitMode = QuitMode.QuitApplication;

        // Start is called before the first frame update
        void Start()
        {
            Application.wantsToQuit += OnWantToQuit;
            DontDestroyOnLoad(gameObject);
            Application.targetFrameRate = 120;

            switch (_start)
            {
                case StartMode.Menu:
                    SceneManager.LoadScene(Constants.TAG_MENUSCENE);
                    break;
                case StartMode.Game:
                    SceneManager.LoadScene(Constants.TAG_GAMESCENE);
                    break;
                case StartMode.None:
                    break;
            }

        }


        private bool OnWantToQuit()
        {
            //var canQuit = string.IsNullOrEmpty(m_LocalLobby?.LobbyID);
            bool canQuit = true;
            if (!canQuit)
            {
                StartCoroutine(LeaveBeforeQuit());
            }
            return canQuit;
        }

        /// <summary>
        ///     In builds, if we are in a lobby and try to send a Leave request on application quit, it won't go through if we're quitting on the same frame.
        ///     So, we need to delay just briefly to let the request happen (though we don't need to wait for the result).
        /// </summary>
        private IEnumerator LeaveBeforeQuit()
        {
            yield return null;
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
