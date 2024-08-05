using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingHomeState : CustomerState
{
    public GoingHomeState(Customer customer, StateMachine stateMachine) : base(customer, stateMachine)
    {
    }

    public override void Enter()
    {
        customer.agent.SetDestination(DayManager.Instance.customerExitPoint.position);
    }

    public override void Exit()
    {
    }

    public override void LateLogicUpdate()
    {
        if (customer.IsDestinationReached())
        {
            Debug.Log($"{customer.gameObject.name} destroying from going home state");
            customer.DestroySelf();
        }
    }

    public override void LogicUpdate()
    {
        
    }

    public override void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }
}
