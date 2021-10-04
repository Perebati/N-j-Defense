using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> avaibleStates;

    public BaseState currentState { get; private set; }

    public event Action<BaseState> OnStateChanged;

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        avaibleStates = states;  
    }

    void Update()
    {
        if (currentState == null)
            currentState = avaibleStates.Values.First();

        var nextState = currentState?.Tick(); 

        if (nextState != null && nextState != nextState.GetType())
        {
            SwitchState(nextState);
        }
    }

    private void SwitchState (Type nextState)
    {
        currentState = avaibleStates[nextState];
        OnStateChanged?.Invoke(currentState);
    }

}
