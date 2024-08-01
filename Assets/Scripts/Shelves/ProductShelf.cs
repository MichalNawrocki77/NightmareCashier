using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Enums;

using UnityEngine;

public class ProductShelf : MonoBehaviour
{
    [SerializeField] ProductSO product;

    public void GetProductFromShelf(ProductType requestedProduct)
    {
        if (product.type != requestedProduct)
        {
            Debug.LogError($"Somehow the customer went to an incorrect product shelf. Wanted {requestedProduct} and have gotten {product.type}");
        }
    }
}
