using Chronoshift.Managers;
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
        public ElemScriptable Element;

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
                selectOption = ElementsController.Instance.elementDB.Length -1;
            }

            UpdateElement(selectOption);
        }

        private void UpdateElement(int selectOption)
        {
            Element = ElementsController.Instance.elementDB[selectOption];
            artworkSprite.sprite = Element.elementSprite;
            nameText.text = Element.elementName;
            descriptionText.text = Element.elementDescription;

            foreach (var item in _colorGUI)
            {
                item.color = Element.elementColor;
            }
        }
    }
}
