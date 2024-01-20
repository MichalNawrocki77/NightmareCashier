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
            currentCoroutine = customer.StartCoroutine(
                customer.checkout.InteractionScript.AddingProductsCoroutine());
        }
    }

    public override void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }
}
