using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStatePain : State<NPCStates>
{
    IMove move;
    IPain pain;
    public NPCStatePain(IMove move, IPain pain)
    {
        this.move = move;
        this.pain = pain;
    }
    public override void Enter()
    {
        base.Enter();
        move.Move(Vector3.zero);
        pain.Pain();
    }

    public override void Exit()
    {
        base.Exit();
        pain.IsInPain = false;
    }
}
