using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStatePursuit : State<NPCStates>
{
    IMove move;
    IFoeDetection foeDetection;
    Transform entity;
    float timePrediction;

    public NPCStatePursuit(IMove move, IFoeDetection foeDetection, Transform entity, float timePrediction)
    {
        this.move = move;
        this.foeDetection = foeDetection;
        this.entity = entity;
        this.timePrediction = timePrediction;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void FixedExecute()
    {
        base.FixedExecute();

        if (!foeDetection.IsCurrentTargetSetAndAlive())
        {
            foeDetection.ClearTarget();
            return;
        }
        move.Move(Pursuit.GetDir(entity, foeDetection.Target.Rb, timePrediction));
    }
}
