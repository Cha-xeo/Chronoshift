using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.VFX;
using UnityEngine.VFX;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //[SerializeField] VisualEffect _vfx;
    Tilemap _tilemap;
    public bool cl = false;

    private void Start()
    {
        _tilemap = TileManager.Instance.GetTileMap(0);
    }
    private void Update()
    {
        if (!IsOwner) return;
        transform.Translate(InputManager.GetInstance().GetMoveDirection() * 2 * Time.deltaTime);
        //_vfx.SetVector3("Sphere pos", transform.position);
        _tilemap.SetColor(_tilemap.WorldToCell(transform.position), cl ? Color.black : Color.white);
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
