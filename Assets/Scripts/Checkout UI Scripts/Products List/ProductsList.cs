using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsList : MonoBehaviour
{
    //Dictionary<Product, int> products_Quantity_Pair;
    Dictionary<Product, Products_ListItem> product_ProductListItem_Pair;

    [SerializeField] GameObject productListItem;
    private void Awake()
    {
        product_ProductListItem_Pair = new Dictionary<Product, Products_ListItem>();
    }
    /// <summary>
    /// Automatically updates Product_ProductListItem_Pair, if new ListItem is supposed to be created
    /// </summary>
    /// <param name="product"></param>
    public void AddProductToProducts_Quantity_Pair(Product product)
    {
        if (product_ProductListItem_Pair.ContainsKey(product))
        {
            product_ProductListItem_Pair[product].IncrementQuantity();
        }
        else 
        {
            product_ProductListItem_Pair[product] = Instantiate(productListItem, transform).GetComponent<Products_ListItem>();
            product_ProductListItem_Pair[product].product = product;
        }
    }
}
