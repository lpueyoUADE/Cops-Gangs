using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateDead : State<NPCStates>
{
    IMove move;  
    
    public NPCStateDead(IMove move)
    {
        this.move = move;
    }
    public override void Enter()
    {
        base.Enter();
        move.Move(Vector3.zero);
    }
}
