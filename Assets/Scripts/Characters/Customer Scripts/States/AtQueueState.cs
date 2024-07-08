using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtQueueState : CustomerState
{
    public AtQueueState(Customer customer, StateMachine stateMachine) : base(customer, stateMachine)
    {
    }

    public override void Enter()
    {
        Checkout temp = DayManager.Instance.selfServiceQueue.
            GetAvailableSelfServiceCheckout();
        
        if(temp is not null)
        {
            customer.AssignCustomerToCheckout(temp);
            return;
        }

        if(DayManager.Instance.selfServiceQueue.customersInQueue.Count
            >=
            DayManager.Instance.selfServiceQueue.queuePositions.Count)
        {
            //If there is no place for him at the queue,let him walk around the store, but since I don't feel like making him walking around the store for now, let him go and collect products again :>
            //also add a strike, because we don't want to let player not care about customers, but we also don't a situation that due to unlucky customer spawns and event triggers a player must take at least one strike
            DayManager.Instance.AddStrike();
            customer.sm.ChangeState(customer.collectingProductsState);
            return;
        }
        else
        {
            customer.agent.SetDestination(
                DayManager.Instance.selfServiceQueue.queuePositions[
                    DayManager.Instance.selfServiceQueue.customersInQueue.Count
                    ].position);
            DayManager.Instance.selfServiceQueue.customersInQueue.Add(customer);
        }        
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

    }

    public override void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }
}
