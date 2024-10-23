using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IState<T>
{
    void Enter();
    void Execute();
    void FixedExecute();
    void LateExecute();
    void Exit();
    void AddTransition(T input, IState<T> state);
    IState<T> GetTransition(T input);
    void RemoveTransition(T input);
    void RemoveTransition(IState<T> state);
}
