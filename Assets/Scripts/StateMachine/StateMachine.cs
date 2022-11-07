using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private State _currentState;

    public State CurrentState => _currentState;

    public void Init(State startState)
    {
        _currentState = startState;
        _currentState.Enter();
    }

    public bool TryGetNextState(out State nextState)
    {
        nextState = null;

        foreach (var option in _currentState.TransitionOptions)
            if (option.Key.DoTransit == true)
                nextState = option.Value;

        return (nextState != null);
    }

    public void ChangeState(State newState)
    {
        foreach (var option in _currentState.TransitionOptions)
            option.Key.ResetDoTransit();

        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
