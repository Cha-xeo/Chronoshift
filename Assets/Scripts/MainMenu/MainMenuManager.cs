using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //[SerializeField] MusicHandler _musicHandler;

    public void Quit()
    {
        #if !UNITY_EDITOR
			 Application.Quit();
#endif

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    void SceneHandler(string name)
    {
        SceneManager.LoadScene(name);
    }
}
