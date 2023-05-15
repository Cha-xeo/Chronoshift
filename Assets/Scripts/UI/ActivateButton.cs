using System;
using UnityEngine;
using UnityEngine.UI;

namespace Chronoshift.UI
{
    public class ActivateButton : MonoBehaviour
    {
        [SerializeField] Button _btn;
        [SerializeField] Image _btnImage;
        public void ActivateButon()
        {
            _btn.interactable = true;
            _btnImage.CrossFadeAlpha(100, 1, true);
        }
    }
}
