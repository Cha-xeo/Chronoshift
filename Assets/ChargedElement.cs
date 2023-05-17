using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChargedElement : MonoBehaviour
{
    public static ChargedElement Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Constants.Elements CurrentElement;
    public List<SpellsScriptable> SpellArray;
    Spells _holdingSpell;
    public bool holding;
    public bool canCast;
    public Vector2 lastPos;

    private void Update()
    {
        if (!holding || !canCast) return;
        if (InputManager.GetInstance().GetLeftMousePressed())
        {
            _holdingSpell.Use();
            holding = false;
            _holdingSpell = null;
        }
        else if (InputManager.GetInstance().GetRightMousePressed())
        {
            holding = false;
            _holdingSpell = null;
        }
    }

    public void HoldSpell(int i)
    {
        holding = true;
        _holdingSpell = SpellArray[i].Spell.GetComponent<Spells>();
    }
}
