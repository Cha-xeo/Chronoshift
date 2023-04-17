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
    [SerializeField] VisualEffect _vfx;
    [SerializeField] InputHandler _input;
    [SerializeField] Tilemap _tilemap;
    [SerializeField] Grid _grid;
    public int ViewRayon;
    Color _color;
    public bool cl = false;

    private void Start()
    {
        _color = new Color(1,1,1,1);
    }

    private readonly Vector3Int[] neighbourPositions =
    {
        Vector3Int.up,
        Vector3Int.right,
        Vector3Int.down,
        Vector3Int.left,
        Vector3Int.up + Vector3Int.right,
        Vector3Int.up + Vector3Int.left,
        Vector3Int.down + Vector3Int.right,
        Vector3Int.down + Vector3Int.left
    };

    private void Update()
    {
        transform.Translate(_input.InputVector * 2 * Time.deltaTime);
        _vfx.SetVector3("Sphere pos", transform.position);
        _tilemap.SetColor(_tilemap.WorldToCell(transform.position), cl ? Color.black : Color.white);
    }

    private void FixedUpdate()
    {
        UpdateFog();
    }

    private void UpdateFog()
    {
        Vector3 posBuf = transform.position;
        /*for (int i = 0; i < ViewRayon; i++)
        {
            _tilemap.SetColor(_tilemap.WorldToCell(transform.position), Color.white);
        }*/
        foreach (var neighbourPosition in neighbourPositions)
        {
            Vector3Int position = _tilemap.WorldToCell(posBuf + neighbourPosition);

            //_tilemap.cel
            if (_tilemap.HasTile(position))
            {
                _tilemap.SetColor((position), cl ? Color.black : Color.white);
            }
        }
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
