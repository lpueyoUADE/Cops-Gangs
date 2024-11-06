using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateFollow : State<NPCStates>
{
    IMove move;
    FlockingManager flockingManager;
    Transform leader;

    public NPCStateFollow(IMove move, FlockingManager flockingManager, Transform leader)
    {
        this.move = move;
        this.flockingManager = flockingManager;
        this.leader = leader;
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
        move.Look(leader);
    }
}
