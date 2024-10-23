using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T>
{
    IState<T> current;

    public Action<T, IState<T>, IState<T>> onTransition = delegate { };
    
    public FSM() { }
    
    public FSM(IState<T> init)
    {
        SetInitial(init);
    }

    public void SetInitial(IState<T> init)
    {
        current = init;
        current.Enter();
    }

    public void OnUpdate()
    {
        if (current != null)
            current.Execute();
    }
    public void OnFixedUpdate()
    {
        if (current != null)
            current.FixedExecute();
    }
    public void OnLateUpdate()
    {
        if (current != null)
            current.LateExecute();
    }

    public void OnExit()
    {
        if (current != null)
            current.Exit();
    }

    public void Transition(T input)
    {
        IState<T> newState = current.GetTransition(input);
        if (newState == null) return;
        var previousState = current;
        current.Exit();
        current = newState;
        current.Enter();
        onTransition(input, current, previousState);
    }

    public IState<T> GetCurrent => current;
}
