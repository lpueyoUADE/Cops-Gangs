using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateFollow : State<NPCStates>
{
    IMove move;
    IFoeDetection foeDetection;

    public NPCStateFollow(IMove move, IFoeDetection foeDetection)
    {
        this.move = move;
        this.foeDetection = foeDetection;
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void FixedExecute()
    {
        base.FixedExecute();
        
        // Follow the leader
        move.Move(new Vector3(1,0,0));

        // Search for enemies
        foeDetection.DetectAliveFoes();
    }
}
