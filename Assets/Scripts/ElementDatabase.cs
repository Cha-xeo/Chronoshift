using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ElementDatabase : ScriptableObject
{
    public SelectElem[] element;
    public int ElemCount
    {
        get {
            return element.Length;
        }
    }

    public SelectElem GetElement(int index)
    {
        return element[index];
    }
}
