using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingToCheckoutState : CustomerState
{
    Coroutine currentCoroutine;
    public GoingToCheckoutState(Customer customer, StateMachine stateMachine) : base(customer, stateMachine)
    {
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        customer.checkout.CustomerCurrent = null;
        customer.checkout = null;
    }

    public override void LateLogicUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void LogicUpdate()
    {
        if (customer.IsDestinationReached() && currentCoroutine is null)
        {
            currentCoroutine = customer.StartCoroutine(InteractWithCheckout());
        }
    }

    public override void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }
    IEnumerator InteractWithCheckout()
    {
        //Debug.Log(customer.name + " zaczyna czekac");

        //for the time being this will just be a random time, but in the future I will replace it with actual logic
        yield return new WaitForSeconds(Random.Range(
            DayManager.Instance.minCustomerWait+5, DayManager.Instance.maxCustomerWait+5));


        DayManager.Instance.AddPoints(10);
        
        //Debug.Log(customer.name + " skonczyl czekac");

        customer.sm.ChangeState(customer.goingHomeState);
        
    }
}
