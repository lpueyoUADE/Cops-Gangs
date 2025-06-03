using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPCStateRunningAway : State<NPCStates>
{
    IMove move;
    Evade evade;
    public NPCStateRunningAway(IMove move, IRunningAway runningAway, Transform entity, Rigidbody target, float timePrediction)
    {
        this.move = move;

        evade = new(entity, target, timePrediction);
    }

    public override void FixedExecute()
    {
        base.FixedExecute();
        Vector3 moveDir = evade.GetDir();
        move.Move(moveDir);
        move.Look(moveDir);
    }
}
