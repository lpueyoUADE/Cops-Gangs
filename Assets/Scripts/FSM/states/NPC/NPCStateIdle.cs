using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateIdle : State<NPCStates>
{
    IMove move;

    public NPCStateIdle(IMove move)
    {
        this.move = move;
    }
    public override void Enter()
    {
        base.Enter();
        move.Move(Vector3.zero);
    }
}
