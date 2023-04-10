using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.VFX;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    
    bool clicked = false;
    [SerializeField] VisualEffect _vfx;
    [SerializeField] InputHandler _input;

    private void Update()
    {
        transform.Translate(_input.InputVector * 2 * Time.deltaTime);
        _vfx.SetVector3("Sphere pos", transform.position);
        //_vfx.SetVector3("Sphere Angles", transform.rotation.eulerAngles);
        //_vfx.SetVector3("Sphere scale");
    }
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
