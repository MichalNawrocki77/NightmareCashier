using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductAisle : MonoBehaviour
{
    ProductShelf[] productShelves;

    // Start is called before the first frame update
    void Start()
    {
        productShelves = GetComponentsInChildren<ProductShelf>();
    }
    /// <summary>
    /// This method gives you a productShelf that a customer is supposed to go to.
    /// </summary>
    /// <returns>A productShelf object from which a customer can collect products</returns>
    public ProductShelf RequestShelf()
    {
        return productShelves[GetNextShelfIndex()];
    }

    /// <summary>
    /// Return an index of the next shelf to give to a customer.
    /// </summary>
    /// <returns>An integer that is an index of the productsShelves array</returns>
    int GetNextShelfIndex()
    {
        Debug.LogWarning("GetNextShelfIndex() gives a random number, change it when you figure out how it should behave");
        return Random.Range(0, productShelves.Length);
    }
}
