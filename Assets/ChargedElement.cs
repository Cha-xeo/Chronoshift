using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedElement : MonoBehaviour
{
    public Constants.Elements CurrentElement;
    [SerializeField] InputHandler _input;
    public List<SpellsScriptable> SpellArray;

    private void Update()
    {
        if (InputManager.GetInstance().GetAPressed()) 
        {
            SpellArray[0].Spell.GetComponent<Spells>().Use();
        }else if (InputManager.GetInstance().GetBPressed())
        {
            Debug.Log(SpellArray[1].elements);
            SpellArray[1].Spell.GetComponent<Spells>().Use();
        }
    }
}
