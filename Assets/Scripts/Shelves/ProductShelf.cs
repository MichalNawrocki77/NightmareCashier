using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Enums;

using UnityEngine;

public class ProductShelf : MonoBehaviour
{
    [SerializeField] protected ProductSO product;

    public virtual int GetProductFromShelf(ProductType requestedProduct, int amount)
    {
        if (product.type != requestedProduct)
        {
            Debug.LogError($"Somehow the customer went to an incorrect product shelf. Wanted {requestedProduct} and have gotten {product.type}. Exection did not stop");
        }
        return amount;
    }
}
