using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chronoshift.Gameplay.UI
{
    public class UISettingsCanvas : MonoBehaviour
    {
        [SerializeField] GameObject _SettingPanelRoot;
        [SerializeField] GameObject _QuitPanelRoot;

        void Awake()
        {
            DisablePanels();
        }

        void DisablePanels()
        {
            _SettingPanelRoot.SetActive(false);
            _QuitPanelRoot.SetActive(false);
        }
        public void OnClickSettingsButton()
        {
            _SettingPanelRoot.SetActive(!_SettingPanelRoot.activeSelf);
            _QuitPanelRoot.SetActive(false);
        }
        public void OnClickQuitButton()
        {
            _QuitPanelRoot.SetActive(!_QuitPanelRoot.activeSelf);
            _SettingPanelRoot.SetActive(false);
        }
    }
}
