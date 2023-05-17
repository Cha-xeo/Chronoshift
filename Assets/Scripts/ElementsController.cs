using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.CinemachineOrbitalTransposer;

public class ElementsController : MonoBehaviour
{
    public static ElementsController Instance { get; private set; }
    [HideInInspector] public List<SpellsScriptable> EarthSpells;
    [HideInInspector] public List<SpellsScriptable> FireSpells;
    [HideInInspector] public List<SpellsScriptable> PlantSpells;
    [HideInInspector] public List<SpellsScriptable> WaterSpells;
    [HideInInspector] public List<SpellsScriptable> WindSpells;

    
    [SerializeField] List<Image> _buttons;
    public ElemScriptable[] elementDB;
    // TODO GetPlayer
    [SerializeField] ChargedElement charged;

    private void Awake()
    {
        Instance = this;
    }

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
        charged.holding = false;
        charged.SpellArray.Clear();
        switch ((Constants.Elements)type)
        {
            case Constants.Elements.Earth:
                for (int i = 0; i < EarthSpells.Count(); i++)
                {
                    SwapElement(EarthSpells[i], i);
                }
                break;
            case Constants.Elements.Fire:
                for (int i = 0; i < FireSpells.Count(); i++)
                {
                    SwapElement(FireSpells[i], i);
                }
                break;
            case Constants.Elements.Plant:
                for (int i = 0; i < PlantSpells.Count(); i++)
                {
                    SwapElement(PlantSpells[i], i);
                }
                break;
            case Constants.Elements.Water:
                for (int i = 0; i < WaterSpells.Count(); i++)
                {
                    SwapElement(WaterSpells[i], i);
                }
                break;
            case Constants.Elements.Wind:
                for (int i = 0; i < WindSpells.Count(); i++)
                {
                    SwapElement(WindSpells[i], i);
                }
                break;
        }
    }

    void SwapElement(SpellsScriptable spell, int i)
    {
        _buttons[i].sprite = spell._icon;
        charged.SpellArray.Add(spell);
    }
}
