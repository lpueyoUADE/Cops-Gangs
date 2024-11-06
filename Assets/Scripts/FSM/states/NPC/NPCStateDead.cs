using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class NPCStateDead : State<NPCStates>
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
        //GameManager.Instance.RollDynamicItem();
    }
}
