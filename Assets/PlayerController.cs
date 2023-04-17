using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.VFX;
using UnityEngine.VFX;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    
    bool clicked = false;
    [SerializeField] InputHandler _input;

    private void Update()
    {
        transform.Translate(_input.InputVector * 2 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        UpdateFog();
    }

    private void UpdateFog()
    {
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
    }
}