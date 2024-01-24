using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Enums;

using UnityEngine;

public class ProductsList : MonoBehaviour
{
    //just a band aid way of giving each ProductListItem a reference to the modifyProductPanel (this script does not need this reference, the other script does and it would be hard to get it otherwise);
    [SerializeField] ModifyProduct_Panel modifyProductPanel;
    Dictionary<Product, Products_ListItem> product_ProductListItem_Pair;
    [SerializeField] GameObject productListItem;
    Dictionary<Product, int> trueQuantity;
    float trueWeight;
    private void Awake()
    {
        product_ProductListItem_Pair = new Dictionary<Product, Products_ListItem>();
        trueQuantity = new Dictionary<Product, int>();
        trueWeight = 0;
    }
    /// <summary>
    /// Automatically updates Product_ProductListItem_Pair, if new ListItem is supposed to be created
    /// </summary>
    /// <param name="product"></param>
    void AddProductToProducts_Quantity_Pair(Product product, InteractionFailureType failureType)
    {
        switch (failureType)    
        {
            case InteractionFailureType.None:
                AddProductWithoutFailure(product);
                break;

            case InteractionFailureType.IncorrectQuantity:
                //nothing is supposed to happen (the product was already spawned inside Side Scale, I just need to omit it when adding it to the ProductsList)
                break;

            case InteractionFailureType.IncorrectWeight:
                AddProductWithWeightFailure(product);
                break;

            default:
                break;
        }

    }
    public void AddProductAsCustomer(Product product, InteractionFailureType failureType)
    {
        AddProductToProducts_Quantity_Pair(product, failureType);
        AddProductToTrueValues(product);
    }
    public void AddProductFromPanel(Product product)
    {
        //Since I add it from Panel it never supposed to have a failure
        AddProductToProducts_Quantity_Pair(product, InteractionFailureType.None);
    }

    void AddProductWithoutFailure(Product product)
    {
        if (product_ProductListItem_Pair.ContainsKey(product))
        {
            product_ProductListItem_Pair[product].IncrementQuantity();
        }
        else
        {
            product_ProductListItem_Pair[product] = Instantiate(productListItem, transform).GetComponent<Products_ListItem>();
            product_ProductListItem_Pair[product].product = product;
            product_ProductListItem_Pair[product].UpdateUIValues();
            product_ProductListItem_Pair[product].modifyProductPanel = modifyProductPanel;
        }
    }
    void AddProductWithWeightFailure(Product product)
    {
        if (product_ProductListItem_Pair.ContainsKey(product))
        {
            product_ProductListItem_Pair[product].IncrementQuantity();
            ScrewUpWeight(product);
        }
        else
        {
            product_ProductListItem_Pair[product] = Instantiate(productListItem, transform).GetComponent<Products_ListItem>();
            product_ProductListItem_Pair[product].product = product;
            product_ProductListItem_Pair[product].UpdateUIValues();
            product_ProductListItem_Pair[product].modifyProductPanel = modifyProductPanel;

            ScrewUpWeight(product);
        }
    }
    void AddProductToTrueValues(Product product)
    {
        //Podczas sprawdzania CheckForFailures wypierdala blad, bo jak koles dodaje produkt w korutynie to wzywasz t¹ metoda, i jak dodajesz recznie produkt to ta metoda tez jest wzywana wiec s¹ zle wartoœci w TrueQuantity. Napraw
        if (trueQuantity.ContainsKey(product))
        {
            trueQuantity[product]++;
        }
        else
        {
            trueQuantity[product] = 1;
        }
        trueWeight += product.weight;
    }
    void ScrewUpWeight(Product product)
    {
        product_ProductListItem_Pair[product].FullWeight += Random.Range(
                -product.weight, product.weight);
    }

    public bool CheckForFailures()
    {
        //float interactionsWeight = 0;
        foreach(KeyValuePair<Product,Products_ListItem> pair in product_ProductListItem_Pair)
        {
            if( pair.Value.Quantity != trueQuantity[pair.Key])
            {
                return true;
            }
            if(pair.Value.FullWeight != trueQuantity[pair.Key] * pair.Key.weight)
            {
                return true;
            }
        }
        //if (trueWeight != interactionsWeight)
        //{
        //    return true;
        //}
        return false;
    }
}
