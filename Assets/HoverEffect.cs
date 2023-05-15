using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    public Material highlightMaterial; // Le matériau pour la mise en évidence du GameObject
    private Material defaultMaterial; // Le matériau par défaut du GameObject

    void Start()
    {
        Debug.Log("one");
        defaultMaterial = GetComponent<Renderer>().material; // Récupère le matériau par défaut du GameObject
    }

    void OnMouseEnter()
    {
        Debug.Log("two");
        GetComponent<Renderer>().material = highlightMaterial; // Applique le matériau de mise en évidence
    }

    void OnMouseExit()
    {
        Debug.Log("three");
        GetComponent<Renderer>().material = defaultMaterial; // Rétablit le matériau par défaut
    }
}