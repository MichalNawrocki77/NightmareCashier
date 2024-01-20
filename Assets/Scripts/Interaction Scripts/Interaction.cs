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
    CanvasGroup canvasGroup;
    
    public bool IsVisible
    {
        get
        {
            return isVisible;
        }
        set
        {
            isVisible = value;
            canvasGroup.alpha = isVisible ? 1 : 0;
        }
    }
    private bool isVisible;

    InteractionFailureType failureType;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Update()
    {
        Debug.Log("Still Breathing");
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
    /// <summary>
    /// Method used for initial product adding (when customer comes up to the checkout) 
    /// </summary>
    /// <param name="product"></param>
    public void AddProductToInteraction(Product product, InteractionFailureType failureType)
    {
        SpawnProductInsideSideScale(DayManager.Instance.products[(int)product.type]);
        productsList.AddProductToProducts_Quantity_Pair(product, failureType);
    }
    /// <summary>
    /// Method used for adding products to product list via AddProduct_Panel
    /// </summary>
    /// <param name="product"></param>
    public void AddProductToProductsList(Product product)
    {
        //since this is supposed to always add a product I can simply pass None, for the failureType enum
        productsList.AddProductToProducts_Quantity_Pair(
            product, InteractionFailureType.None);
    }
    /// <summary>
    /// returns true if failure has occured
    /// </summary>
    /// <returns></returns>
    public bool GetFailureStatus()
    {
        return failureType != InteractionFailureType.None;
    }
    public IEnumerator AddingProductsCoroutine()
    {
        foreach (Product product in customer.products)
        {
            
            //so far the wait in between product adding is 2s, define it in DayManager in the future
            yield return new WaitForSeconds(2);

            if(
                Random.Range(1, 101)
                <
                DayManager.Instance.chancesOfInteractionFailuresOccuring[0])
            {
                failureType = InteractionFailureType.IncorrectQuantity;
            }
            else if (
                Random.Range(1, 101)
                <
                DayManager.Instance.chancesOfInteractionFailuresOccuring[1])
            {
                failureType = InteractionFailureType.IncorrectWeight;
            }
            else
            {
                failureType = InteractionFailureType.None;
            }

            AddProductToInteraction(product, failureType);
               

            yield return new WaitWhile(GetFailureStatus);
        }
    }
}
