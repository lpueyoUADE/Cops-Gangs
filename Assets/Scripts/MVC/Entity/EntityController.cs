using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityController<T> : MonoBehaviour where T : Enum, IComparable
{
    protected FSM<T> fsm;

    protected IIdle _idle;
    protected IMove _move;
    protected IAttack _attack;
    protected IReload _reload;
    protected IPain _pain;
    protected IDead _dead;
    protected T entity;

    protected Dictionary<T, IState<T>> statesDict;
    /// <summary>
    /// Recorre el diccionario de estados y genera las transiciones entre todos los estados 
    /// exceptuando un estado consigo mismo.
    /// Se asume que se puede transicionar de cualquier estado a otro.
    /// </summary>
    private void GenerateStateTransitions()
    {
        foreach (var statei in statesDict)
        {
            foreach (var statej in statesDict)
            {
                if (statei.Key.CompareTo(statej.Key) == 0)
                    continue;

                statesDict[statei.Key].AddTransition(statej.Key, statej.Value);
            }
        }
    }

    protected virtual void Start()
    {
        InitFSM();
        GenerateStatesDictionary();
        GenerateStateTransitions();
        SetInitialState();
    }
    /// <summary>
    /// Crea una nueva instancia de la FSM y define las interfaces de comportamiento
    /// comunes a todas las entidades.
    /// </summary>
    protected virtual void InitFSM()
    {
        _idle = GetComponent<IIdle>();
        _move = GetComponent<IMove>();
        _attack = GetComponent<IAttack>();
        _reload = GetComponent<IReload>();
        _pain = GetComponent<IPain>();
        _dead = GetComponent<IDead>();

        fsm = new();
    }
    /// <summary>
    /// Esta funcion es la encargada de setear el diccionario de estados.
    /// </summary>
    protected abstract void GenerateStatesDictionary();
    /// <summary>
    /// Funcion que setea el estado Inicial de la FSM.
    /// </summary>
    protected abstract void SetInitialState();
    protected virtual void Update()
    {
        fsm.OnUpdate();
    }
    void FixedUpdate()
    {
        fsm.OnFixedUpdate();
    }
    void LateUpdate()
    {
        fsm.OnLateUpdate();
    }
}
