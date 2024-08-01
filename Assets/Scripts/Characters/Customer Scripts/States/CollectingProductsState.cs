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

    Queue< KeyValuePair<Product,ProductShelf> > productShelvesQueue;
    public CollectingProductsState(Customer customer, StateMachine stateMachine) : base(customer, stateMachine)
    {
    }

    public override void Enter()
    {
        InitializeShelves();
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
        throw new System.NotImplementedException();
    }

    void InitializeShelves()
    {
        productShelvesQueue = new Queue<KeyValuePair<Product,ProductShelf>>();
        foreach (Product item in customer.products.Keys)
        {
            //if (productShelvesQueue.Contains(
            //    DayManager.Instance.productShelves[(int)item.type]))
            //{
            //    continue;
            //}
            productShelvesQueue.Enqueue(new KeyValuePair<Product, ProductShelf>(
                item, DayManager.Instance.productShelves[(int)item.type])
                );
                
        }
    }
    void SetNextDestination()
    {
        customer.agent.SetDestination(currentDestination.Value.transform.position);
    }
    void GetNewProductShelfAndSetDestination()
    {
        try
        {
            KeyValuePair<Product, ProductShelf> temp = productShelvesQueue.Dequeue();
            currentDestination = temp;
            SetNextDestination();
        }
        catch (InvalidOperationException)
        {
            customer.sm.ChangeState(customer.goingToQueueState);
        }
        
    }
    IEnumerator CustomerReachedShelfCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(
            DayManager.Instance.minCustomerShelfWait, DayManager.Instance.maxCustomerShelfWait));

        currentDestination.Value.GetProductFromShelf(currentDestination.Key.type);

        GetNewProductShelfAndSetDestination();

        currentNavigationCoroutine = null;
    }
}