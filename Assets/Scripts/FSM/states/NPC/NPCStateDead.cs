using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateDead : State<FollowerStates>
{
    IMove move;
    IDead dead;

    public NPCStateDead(IMove move, IDead dead)
    {
        this.move = move;
        this.dead = dead;
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
