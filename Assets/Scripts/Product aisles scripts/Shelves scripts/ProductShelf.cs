using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Enums;

using Unity.VisualScripting;

using UnityEngine;

public class ProductShelf : MonoBehaviour
{
    [SerializeField] protected ProductSO product;
    public Vector3 CollectProductPosition { get; protected set; }

    protected SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = product.sprite;
        
        CollectProductPosition = transform.GetChild(0).GetComponent<Transform>().position;
    }
    public virtual int GetProductFromShelf(ProductType requestedProduct, int amount)
    {
        if (product.type != requestedProduct)
        {
            Debug.LogError($"Somehow the customer went to an incorrect product shelf. Wanted {requestedProduct} and have gotten {product.type}. Exection did not stop");
        }
        return amount;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ProductShelf"))
        {
            Debug.LogError("Two ProductShelves colliders touch. Possible bugs with setting players InteractionAction!!!");
        }
    }
}
