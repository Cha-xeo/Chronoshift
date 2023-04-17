using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour, ISpells
{
    public Constants.Elements Element;
    public virtual void Use() 
    {
        Debug.Log(Element.ToString());
    }
}
