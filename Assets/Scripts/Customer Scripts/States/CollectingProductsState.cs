using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Enums;
using Unity.VisualScripting;

using UnityEditor.SearchService;

using UnityEngine;

using Random = UnityEngine.Random;

public class CollectingProductsState : CustomerState
{

    public Coroutine currentNavigationCoroutine;

    Queue<Transform> productShelvesQueue;
    public CollectingProductsState(Customer customer, StateMachine stateMachine) : base(customer, stateMachine)
    {
    }

    public override void Enter()
    {
        InitializeShelves();
        SetNextDestination();
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
            currentNavigationCoroutine = customer.StartCoroutine(WaitForNextDestination());
        }
    }
    public override void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }

    void InitializeShelves()
    {
        productShelvesQueue = new Queue<Transform>();
        foreach (Product item in customer.products)
        {
            if (productShelvesQueue.Contains(DayManager.Instance.productShelves[(int)item.type]))
            {
                continue;
            }
            productShelvesQueue.Enqueue(DayManager.Instance.productShelves[(int)item.type]);
        }
    }
    void SetNextDestination()
    {
        try
        {
            customer.agent.SetDestination(productShelvesQueue.Dequeue().position);
        } catch (InvalidOperationException)
        {
            customer.sm.ChangeState(customer.goingToQueueState);
        }
        
    }

    IEnumerator WaitForNextDestination()
    {
        yield return new WaitForSeconds(Random.Range(
            DayManager.Instance.minCustomerShelfWait, DayManager.Instance.maxCustomerShelfWait));
        SetNextDestination();
        currentNavigationCoroutine = null;
    }
}