using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T> : IState<T>
{
    Dictionary<T, IState<T>> transitions = new Dictionary<T, IState<T>>();

    public virtual void Enter()
    {

    }

    public virtual void Execute()
    {

    }

    public virtual void FixedExecute()
    {

    }

    public virtual void LateExecute()
    {

    }

    public virtual void Exit()
    {

    }

    public void AddTransition(T input, IState<T> state)
    {
        transitions[input] = state;
    }

    public IState<T> GetTransition(T input)
    {
        if (!transitions.ContainsKey(input)) return null;
        return transitions[input];
    }

    public void RemoveTransition(T input)
    {
        if (transitions.ContainsKey(input))
        {
            transitions.Remove(input);
        }
    }

    public void RemoveTransition(IState<T> state)
    {
        foreach (var item in transitions)
        {
            if (state == item.Value)
            {
                transitions.Remove(item.Key);
                break;
            }
        }
    }
}
