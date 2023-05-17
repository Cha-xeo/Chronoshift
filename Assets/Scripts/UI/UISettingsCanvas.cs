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
        [SerializeField] GameObject _buttonPanelRoot;
        [SerializeField] GameObject _panelRoot;
        [SerializeField] GameObject _audioPanelRoot;
        [SerializeField] GameObject _quitPanelRoot;
        bool _state;

        private void Update()
        {
            if (InputManager.GetInstance().GetEscapePressed())
            {
                _state = false;
                _buttonPanelRoot.SetActive(true);
                _audioPanelRoot.SetActive(false);
                _quitPanelRoot.SetActive(false);
                _panelRoot.SetActive(!_panelRoot.activeSelf);
            }
        }


        public void OnClickAudioButton()
        {
            _state = true;
            _buttonPanelRoot.SetActive(false);
            _audioPanelRoot.SetActive(true);

        }
        public void OnClickQuitButton()
        {
            _state = true;
            _buttonPanelRoot.SetActive(false);
            _quitPanelRoot.SetActive(true);
        }

        public void OnClickCloseButton() 
        {
            if (_state)
            {
                _audioPanelRoot.SetActive(false);
                _quitPanelRoot.SetActive(false);
                _buttonPanelRoot.SetActive(true);
                _state = false;
                return;
            }
            _panelRoot.SetActive(false);
        }
    }
}
