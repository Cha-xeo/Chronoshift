using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chronoshift.UI
{
    public class ElementSeletionUI : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descriptionText;
        public Image artworkSprite;
        [SerializeField] Image[] _colorGUI;
        [SerializeField] GameObject _playerHUD;
        [SerializeField] GameObject _managers;

        private int selectOption = 0;

        void Start()
        {
            UpdateElement(selectOption);
        }

        public void NextOption()
        {
            selectOption++;
            if (selectOption >= ElementsController.Instance.elementDB.Length)
            {
                selectOption = 0;
            }

            UpdateElement(selectOption);
        }

        public void BackOption()
        {
            selectOption--;
            if (selectOption < 0)
            {
                selectOption = ElementsController.Instance.elementDB.Length - 1;
            }

            UpdateElement(selectOption);
        }

        private void UpdateElement(int selectOption)
        {
            ElemScriptable element = ElementsController.Instance.elementDB[selectOption];
            artworkSprite.sprite = element.elementSprite;
            nameText.text = element.elementName;
            descriptionText.text = element.elementDescription;

            foreach (var item in _colorGUI)
            {
                item.color = element.elementColor;
            }
        }

        public void PlayButton()
        {
            _playerHUD.SetActive(true);
            ElementsController.Instance.ChangeElement(selectOption);
            _managers.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
