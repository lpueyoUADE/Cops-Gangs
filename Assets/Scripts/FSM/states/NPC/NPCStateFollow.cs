using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateFollow : State<NPCStates>
{
    IMove move;
    IFoeDetection foeDetection;
    FlockingManager flockingManager;

    public NPCStateFollow(IMove move, IFoeDetection foeDetection, FlockingManager flockingManager)
    {
        this.move = move;
        this.foeDetection = foeDetection;
        this.flockingManager = flockingManager;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void FixedExecute()
    {
        base.FixedExecute();
        var dir = flockingManager.GetDir();
        move.Move(dir);
    }
}
