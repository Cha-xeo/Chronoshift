using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElementManager : MonoBehaviour
{
    public ElementDatabase elementDB;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public SpriteRenderer artworkSprite;

    private int selectOption = 0;

    void Start()
    {
        UpdateElement(selectOption);
    }

    public void NextOption()
    {
        selectOption++;
        if (selectOption >= elementDB.ElemCount)
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
            selectOption = elementDB.ElemCount - 1;
        }

        UpdateElement(selectOption);
    }

    private void UpdateElement(int selectOption)
    {
        SelectElem element = elementDB.GetElement(selectOption);
        artworkSprite.sprite = element.elementSprite;
        nameText.text = element.elementName;
        descriptionText.text = element.elementDescription;
    }

}
