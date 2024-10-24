using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderStatePain : State<PlayerStates>
{
    FSM<PlayerStates> fsm;
    public RyderStatePain(FSM<PlayerStates> fsm)
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
