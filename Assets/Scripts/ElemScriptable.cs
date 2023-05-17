using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New element", menuName = "Scriptable element")]
public class ElemScriptable : ScriptableObject
{
    public Constants.Elements Elements;
    public string elementName;
    public string elementDescription;
    public Sprite elementSprite;
    public Color elementColor;
}