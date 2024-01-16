using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;

using UnityEngine;

public class Interaction : MonoBehaviour
{
    internal Customer customer;
    internal Player player;
    [SerializeField] RectTransform sideScaleRect;
    void Start()
    {
        foreach (ProductType item in customer.products)
        {
            SpawnProductInsideSideScale(DayManager.Instance.products[(int)item]);
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
}