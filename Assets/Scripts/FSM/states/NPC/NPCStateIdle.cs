using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateIdle : State<NPCStates>
{
    IMove move;
    IFoeDetection foeDetection;

    public NPCStateIdle(IMove move, IFoeDetection foeDetection)
    {
        this.move = move;
        this.foeDetection = foeDetection;
    }
    public override void Enter()
    {
        base.Enter();
        move.Move(Vector3.zero);
    }

    public override void FixedExecute()
    {
        base.FixedExecute();
    }
}
