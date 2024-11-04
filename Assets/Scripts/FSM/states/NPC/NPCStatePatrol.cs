using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStatePatrol : State<NPCStates>
{
    IMove move;
    IFoeDetection foeDetection;
    public NPCStatePatrol(IMove move, IFoeDetection foeDetection)
    {
        this.move = move;
        this.foeDetection = foeDetection;
    }

    public override void FixedExecute()
    {
        base.FixedExecute();
    }
}
