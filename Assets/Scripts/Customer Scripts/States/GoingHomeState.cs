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
        throw new System.NotImplementedException();
    }

    public override void LogicUpdate()
    {
        if (customer.IsDestinationReached())
        {
            customer.DestroySelf();
        }
    }

    public override void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }
}
