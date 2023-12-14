using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public List<GameObject> products;
    // Start is called before the first frame update
    void Start()
    {
        InitializeItems();
        foreach(GameObject item in products)
        {
            Debug.Log(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeItems()
    {
        products = new List<GameObject>();
        for(int i=0; i<Random.Range(3,7); i++)
        {
            products.Add(DayManager.Instance.products[
                Random.Range(0,DayManager.Instance.products.Count)]
                );
        }
    }
}
