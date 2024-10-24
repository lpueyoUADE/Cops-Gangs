using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityController<T> : MonoBehaviour where T: Enum
{
    protected FSM<T> fsm;
    protected LineOfSight lineOfSight;

    protected IIdle _idle;
    protected IMove _move;
    protected IPain _pain;
    protected IDead _dead;

    private void Awake()
    {
        lineOfSight = GetComponent<LineOfSight>();
    }
    protected virtual void Start()
    {
        InitFSM();
    }

    protected virtual void InitFSM()
    {
        _idle = GetComponent<IIdle>();
        _move = GetComponent<IMove>();
        _pain = GetComponent<IPain>();
        _dead = GetComponent<IDead>();
    }

    void Update()
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
