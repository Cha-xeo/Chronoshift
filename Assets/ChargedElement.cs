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
        if (InputManager.GetInstance().GetFirePressed()) 
        {
            SpellArray[0].Spell.GetComponent<Spells>().Use();
        }else if (InputManager.GetInstance().GetLookPressed())
        {
            SpellArray[1].Spell.GetComponent<Spells>().Use();
        }
    }
}
