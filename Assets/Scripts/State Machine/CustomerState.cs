using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomerState
{
    internal Customer customer;
    internal StateMachine stateMachine;
    public CustomerState(Customer customer, StateMachine stateMachine)
    {
        this.customer = customer;
        this.stateMachine = stateMachine;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void LateLogicUpdate();
}