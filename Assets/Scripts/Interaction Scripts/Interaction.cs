using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;

using Unity.VisualScripting;

using UnityEngine;

public class Interaction : MonoBehaviour
{
    internal Customer customer;
    internal Player player;
    [SerializeField] RectTransform sideScaleRect;
    [SerializeField] ProductsList productsList;
    void Start()
    {
        foreach (Product product in customer.products)
        {
            AddProductToInteraction(product);
        }
    }
    internal void InjectDependencies(Customer customer, Player player)
    {
        this.customer = customer;
        this.player = player;
    }
    void SpawnProductInsideSideScale(GameObject itemToSpawn)
    {
        //Spawns itemToSpan (which is a prefab, prefferably a prefab of an item xD)
        RectTransform spawnedItem = Instantiate(itemToSpawn, sideScaleRect).
            GetComponent<RectTransform>();

        //Randomizes it's position (but contraints the position, to the rectangle that is "sideScaleRect" which is the "actual scale" object in the hierarchy
        spawnedItem.anchoredPosition = new Vector2(
            Random.Range(
                    (-sideScaleRect.rect.width / 2) - (-spawnedItem.rect.width / 2),
                    (sideScaleRect.rect.width / 2) - (spawnedItem.rect.width / 2)
                ),
            Random.Range(
                    (-sideScaleRect.rect.height / 2) - (-spawnedItem.rect.height / 2),
                    (sideScaleRect.rect.height / 2) - (spawnedItem.rect.height / 2)
                )
            );
    }
    public void AddProductToInteraction(Product product)
    {
        SpawnProductInsideSideScale(DayManager.Instance.products[(int)product.type]);
        productsList.AddProductToProducts_Quantity_Pair(product);
    }
}
