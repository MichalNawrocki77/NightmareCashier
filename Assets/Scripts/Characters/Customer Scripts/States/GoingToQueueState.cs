using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingToQueueState : CustomerState
{
    public GoingToQueueState(Customer customer, StateMachine stateMachine) : base(customer, stateMachine)
    {
    }

    public override void Enter()
    {
        customer.queueManagerEntered += OnQueueManagerEntered;
        customer.agent.SetDestination(DayManager.Instance.selfServiceQueue.transform.position);
    }

    public override void Exit()
    {
        customer.queueManagerEntered -= OnQueueManagerEntered;
    }

    public override void LateLogicUpdate()
    {

    }

    public override void LogicUpdate()
    {
        
    }

    public override void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }
    void OnQueueManagerEntered(Collider2D collider)
    {
        customer.sm.ChangeState(customer.atQueueState);
    }
}
