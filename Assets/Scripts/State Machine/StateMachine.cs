using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public CustomerState currentState;
    public void Initialize(CustomerState newState)
    {
        currentState = newState;
        newState.Enter();
    }
    public void ChangeState(CustomerState newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }

}
