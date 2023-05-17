using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChronoManager;

public abstract class Spells : MonoBehaviour, ISpells
{
    public ElemScriptable Element;
    public BaseChar flag;
    private void Awake()
    {
        Debug.Log("aaa " + Element.name);
        flag.GetComponent<SpriteRenderer>().sprite = Element.elementSprite;
    }
    public virtual void Use() 
    {
        Debug.Log(Element.ToString());
    }
    public virtual void ChronoUse(Vector2 pos) 
    {
        Debug.Log("ChronoUse: " + Element.ToString());
    }
}
