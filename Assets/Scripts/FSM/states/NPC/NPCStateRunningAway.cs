using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateRunningAway : State<NPCStates>
{
    IMove move;
    public NPCStateRunningAway(IMove move)
    {
        this.move = move;
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void FixedExecute()
    {
        base.FixedExecute();
        move.Move(Vector3.forward);
    }
}
