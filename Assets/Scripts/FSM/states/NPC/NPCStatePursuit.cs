using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStatePursuit : State<NPCStates>
{
    IMove move;
    public NPCStatePursuit(IMove move)
    {
        this.move = move;
    }
}
