using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New spell", menuName = "Scriptable spells")]
public class SpellsScriptable : ScriptableObject
{
    
    public Constants.Elements elements;
    public string _name;
    public string _description;
    public Sprite _icon;
    public GameObject Spell;

    public void UsePrefab()
    {
        Spell.GetComponent<Spells>().Element = elements;
        Spell.GetComponent<Spells>().Use();
    }
}
