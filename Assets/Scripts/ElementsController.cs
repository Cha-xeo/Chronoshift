using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ElementsController : MonoBehaviour
{
    [HideInInspector] public List<SpellsScriptable> EarthSpells;
    [HideInInspector] public List<SpellsScriptable> FireSpells;
    [HideInInspector] public List<SpellsScriptable> PlantSpells;
    [HideInInspector] public List<SpellsScriptable> WaterSpells;
    [HideInInspector] public List<SpellsScriptable> WindSpells;

    
    [SerializeField] List<Image> _buttons;
    [SerializeField] ChargedElement charged;

    // Start is called before the first frame update
    void Start()
    {
        EarthSpells = Resources.LoadAll<SpellsScriptable>("Spells/Earth").ToList();
        FireSpells = Resources.LoadAll<SpellsScriptable>("Spells/Fire").ToList();
        PlantSpells = Resources.LoadAll<SpellsScriptable>("Spells/Plant").ToList();
        WaterSpells = Resources.LoadAll<SpellsScriptable>("Spells/Water").ToList();
        WindSpells = Resources.LoadAll<SpellsScriptable>("Spells/Wind").ToList();
    }

    public void ChangeElement(int type)
    {
        charged.CurrentElement = (Constants.Elements)type;
        switch ((Constants.Elements)type)
        {
            case Constants.Elements.Earth:
                for (int i = 0; i < EarthSpells.Count(); i++)
                {
                    _buttons[i].sprite = EarthSpells[i]._icon;
                    charged.SpellArray[i] = EarthSpells[i];

                }
                break;
            case Constants.Elements.Fire:
                for (int i = 0; i < FireSpells.Count(); i++)
                {
                    _buttons[i].sprite = FireSpells[i]._icon;
                    charged.SpellArray[i] = FireSpells[i];
                }
                break;
            case Constants.Elements.Plant:
                for (int i = 0; i < PlantSpells.Count(); i++)
                {
                    _buttons[i].sprite = PlantSpells[i]._icon;
                    charged.SpellArray[i] = PlantSpells[i];

                }
                break;
            case Constants.Elements.Water:
                for (int i = 0; i < WaterSpells.Count(); i++)
                {
                    _buttons[i].sprite = WaterSpells[i]._icon;
                    charged.SpellArray[i] = WaterSpells[i];

                }
                break;
            case Constants.Elements.Wind:
                for (int i = 0; i < WindSpells.Count(); i++)
                {
                    _buttons[i].sprite = WindSpells[i]._icon;
                    charged.SpellArray[i] = WindSpells[i];

                }
                break;
        }
    }
}
