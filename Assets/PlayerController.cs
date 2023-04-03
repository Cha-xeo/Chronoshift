using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    bool clicked = false;
    
    /*private void Update()
    {

    }*/
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
