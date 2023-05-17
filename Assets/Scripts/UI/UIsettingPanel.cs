using System;
using UnityEngine;

namespace Chronoshift.UI
{
    public class UIsettingPanel : MonoBehaviour
    {
        private void OnEnable()
        {
            AplicationController.AplicationController.Instance.gameIsPaused = true;
            Time.timeScale = 0f;
        }

        private void OnDisable()
        {
            AplicationController.AplicationController.Instance.gameIsPaused = false;
            Time.timeScale = 1;
        }
    }
}
