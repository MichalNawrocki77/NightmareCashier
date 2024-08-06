using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Enums;

using UnityEngine;

using Random = UnityEngine.Random;

public class CollectingProductsState : CustomerState
{
    public Coroutine currentNavigationCoroutine;
    KeyValuePair<Product, ProductShelf> currentDestination;

    Queue< KeyValuePair<Product,ProductAisle> > productAislesQueue;
    public CollectingProductsState(Customer customer, StateMachine stateMachine) : base(customer, stateMachine)
    {
    }

    public override void Enter()
    {
        InitializeShelvesQueue();
        GetNewProductShelfAndSetDestination();
    }
    public override void Exit()
    {
    }
    public override void LateLogicUpdate()
    {
    }
    public override void LogicUpdate()
    {
        if (customer.IsDestinationReached() && currentNavigationCoroutine is null)
        {
            currentNavigationCoroutine = customer.StartCoroutine(CustomerReachedShelfCoroutine());
        }
    }
    public override void PhysicsUpdate()
    {
    }

    void InitializeShelvesQueue()
    {
        productAislesQueue = new Queue<KeyValuePair<Product,ProductAisle>>();
        foreach (Product item in customer.productsWanted.Keys)
        {
            //if (productShelvesQueue.Contains(
            //    DayManager.Instance.productShelves[(int)item.type]))
            //{
            //    continue;
            //}
            productAislesQueue.Enqueue(new KeyValuePair<Product, ProductAisle>(
                item,
                DayManager.Instance.productAisles[(int)item.type])
                );
                
        }
    }
    void GetNewProductShelfAndSetDestination()
    {
        try
        {
            KeyValuePair<Product, ProductAisle> temp = productAislesQueue.Dequeue();
            currentDestination = new KeyValuePair<Product, ProductShelf>(
                temp.Key,
                temp.Value.RequestShelf());
            customer.agent.SetDestination(currentDestination.Value.CollectProductPosition);
        }
        catch (InvalidOperationException)
        {
            EvaluateCollectedProducts();
        }
        
    }

    private void EvaluateCollectedProducts()
    {
        //If the productsFound has zero keys, meaning no products were found
        if(customer.productsFound.Count == 0)
        {
            HandleNoProductsFound();
            return;
        }
        if (!customer.CheckIfAllProdcutsWereFound())
        {
            HandleSomeProductsFound();
            return;
        }
        HandleAllProductsFound();
    }

    private void HandleAllProductsFound()
    {
        customer.sm.ChangeState(customer.goingToQueueState);
    }

    private void HandleSomeProductsFound()
    {
        customer.sm.ChangeState(customer.goingToQueueState);
    }

    private void HandleNoProductsFound()
    {
        customer.StopCoroutine(currentNavigationCoroutine);
        currentNavigationCoroutine = null;
        customer.sm.ChangeState(customer.goingHomeState);
    }

    /// <summary>
    /// Indicates that the customer did not recieve the desired amount of product
    /// </summary>
    /// <param name="productRecieved">The amount of product recieved in case you want to use it</param>
    void IndicateIncorrectProductAmount(int productRecieved)
    {
        Debug.LogError("IndicateIncorrectProductAmount() has not been implemented yet");
    }
    IEnumerator CustomerReachedShelfCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(
            DayManager.Instance.minCustomerShelfWait,
            DayManager.Instance.maxCustomerShelfWait));

        int productRecieved = currentDestination.Value.GetProductFromShelf(currentDestination.Key.type,
            customer.productsWanted[currentDestination.Key]);

        //check if the customer has gotten the right amount to indicate errors/give strikes
        if (productRecieved != customer.productsWanted[currentDestination.Key])
        {
            DayManager.Instance.AddStrike();
            IndicateIncorrectProductAmount(productRecieved);
        }

        for(int i = 0; i < productRecieved; i++)
        {
            customer.AddProductToDictionary(
                currentDestination.Key,
                ref customer.productsFound);
        }
        GetNewProductShelfAndSetDestination();
        currentNavigationCoroutine = null;
    }
}