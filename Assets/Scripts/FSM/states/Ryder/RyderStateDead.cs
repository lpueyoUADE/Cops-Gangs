using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderStateDead : State<PlayerStates>
{
    FSM<PlayerStates> fsm;
    public RyderStateDead(FSM<PlayerStates> fsm)
    {
        this.fsm = fsm;

    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void FixedExecute()
    {
        base.FixedExecute();
    }
}
