using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownModel : NPCModel, IRunningAway
{
    bool isRunningAway;
    
    public static Action ClownDeadAction;

    public bool IsRunningAway { get => isRunningAway; set => isRunningAway = value; }

    protected override void Awake()
    {
        base.Awake();
        SetTarget(FindAnyObjectByType<RyderModel>());

        IsRunningAway = false;
    }

    public override void Die()
    {
        base.Die();
        ClownDeadAction?.Invoke();
    }

    protected override void Update()
    {
        base.Update();
        // TODO: Remover
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReceiveDamage(1);
        }
    }

    public void RunningAway()
    {
        IsRunningAway = true;
    }
}
