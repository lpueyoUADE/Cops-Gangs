using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStatePatrol : State<NPCStates>
{
    IMove move;
    public NPCStatePatrol(IMove move)
    {
        this.move = move;
    }
}
