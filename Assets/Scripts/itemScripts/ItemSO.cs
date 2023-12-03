using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ItemSO : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Sprite spriteInInventory;
    SpriteRenderer spriteRenderer;
    //przekminie pozniej co to za warning mi wylatuje
    Collider2D collider;
    Vector3 initialPositionOnBeginDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPositionOnBeginDrag = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       //eventData.pointerCurrentRaycast.gameObject;
        
    }
    
}
