using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

using Assets.Scripts.Enums;

using Unity.VisualScripting;

using UnityEngine;
using Random = UnityEngine.Random;

public class Interaction : MonoBehaviour
{
    Checkout checkout;
    Customer customer;
    Player player;

    CanvasGroup canvasGroup;

    [SerializeField] RectTransform sideScaleRect;
    [SerializeField] ProductsList productsList;
    [SerializeField] DialogueManager dialogueManager;
    
    
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
            canvasGroup.blocksRaycasts = isVisible;
        }
    }
    private bool isVisible;

    [HideInInspector]
    public bool isAcceptClicked;

    InteractionFailureType failureType;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        isAcceptClicked = false;
    }
    internal void InjectDependencies(Customer customer, Player player, Checkout checkout)
    {
        this.customer = customer;
        this.player = player;
        this.checkout = checkout;

        dialogueManager.InjectDependencies(this.customer);
    }
    #region Adding Products
    void SpawnProductInsideSideScale(GameObject itemToSpawn)
    {
        //Spawns itemToSpawn (which is a prefab, prefferably a prefab of an item xD)
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
        productsList.AddProductAsCustomer(product, failureType);
    }
    /// <summary>
    /// Method used for adding products to product list via AddProduct_Panel
    /// </summary>
    /// <param name="product"></param>
    public void AddProductToProductsList(Product product)
    {
        //since this is supposed to always add a product I can simply pass None, for the failureType enum
        //????????????????????? where none? XD
        productsList.AddProductFromPanel(product);
    }
    #endregion
    public IEnumerator CustomerInteractingCoroutine()
    {
        foreach (Product product in customer.productsFound.Keys)
        {
            for (int i = 0; i < customer.productsFound[product]; i++)
            {
                //so far the wait in between product adding is 2s, define it in DayManager in the future
                Debug.LogWarning("Hardcoded value (wait in between adding products), DEFINE IT SOMEWHERE!!!");
                yield return new WaitForSeconds(2);

                if (
                    Random.Range(1, 101)
                    <
                    DayManager.Instance.chancesOfInteractionFailuresOccuring[0])
                {
                    failureType = InteractionFailureType.IncorrectWeight;
                    Debug.Log(failureType);
                }
                else if (
                    Random.Range(1, 101)
                    <
                    DayManager.Instance.chancesOfInteractionFailuresOccuring[1])
                {
                    failureType = InteractionFailureType.IncorrectQuantity;
                    Debug.Log(failureType);
                }
                else
                {
                    failureType = InteractionFailureType.None;
                    Debug.Log(failureType);
                }

                AddProductToInteraction(product, failureType);

            }
        }

        if (productsList.CheckForFailures() == false)
        {
            //If customer didn't have any problems, resolve with following
            customer.sm.ChangeState(customer.goingHomeState);
            player.EnableMovement();
            checkout.DestroyInteraction();
            yield break;
        }

        customer.SetShowingFailureIndicator(true);
        yield return new WaitUntil(() => isAcceptClicked);

        if (productsList.CheckForFailures())
        {
            //Code that resolves interaction if failure is detected after accepting purchase
            DayManager.Instance.AddStrike();
            customer.SetShowingFailureIndicator(false);
            customer.sm.ChangeState(customer.goingHomeState);
            player.EnableMovement();

            checkout.DestroyInteraction();
            yield break;
        }
        //If failure wasnt detected after accepting purchase resolve with following code 
        customer.SetShowingFailureIndicator(false);
        customer.sm.ChangeState(customer.goingHomeState);
        player.EnableMovement();

        checkout.DestroyInteraction();
        
    }
}
