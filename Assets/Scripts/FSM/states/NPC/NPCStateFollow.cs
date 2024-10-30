using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateFollow : State<FollowerStates>
{
    IMove move;

    public NPCStateFollow(IMove move)
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
        move.Move(new Vector3(1,0,0));
    }
}
