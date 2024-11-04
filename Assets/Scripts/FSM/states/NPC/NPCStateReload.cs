using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class NPCStateReload : State<NPCStates>
{
    IMove move;
    IReload reload;
    IFoeDetection foeDetection;
    Transform entity;
    float timePrediction;

    public NPCStateReload(IMove move, IReload reload, IFoeDetection foeDetection, Transform entity, float timePrediction)
    {
        this.move = move;
        this.reload = reload;
        this.foeDetection = foeDetection;
        this.entity = entity;
        this.timePrediction = timePrediction;
    }
    public override void Enter()
    {
        base.Enter();
        reload.Reload();
    }

    public override void FixedExecute()
    {
        base.FixedExecute();

        if (!foeDetection.IsCurrentTargetSetAndAlive())
        {
            foeDetection.ClearTarget();
            return;
        }

        move.Look(Pursuit.GetDir(entity, foeDetection.Target.Rb, timePrediction));
    }
}
