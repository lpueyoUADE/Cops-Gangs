using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderStateAttack : State<PlayerStates>
{
    FSM<PlayerStates> fsm;
    public RyderStateAttack(FSM<PlayerStates> fsm)
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
