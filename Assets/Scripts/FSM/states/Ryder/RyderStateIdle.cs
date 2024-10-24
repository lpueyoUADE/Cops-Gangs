using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderStateIdle : State<PlayerStates>
{
    FSM<PlayerStates> fsm;
    IMove move;
    public RyderStateIdle(FSM<PlayerStates> fsm, IMove move)
    {
        this.fsm = fsm;
        this.move = move;
    }
    public override void Enter()
    {
        base.Enter();
        move.Move(Vector3.zero);
    }

    public override void FixedExecute()
    {
        base.FixedExecute();

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            fsm.Transition(PlayerStates.Run);
        }
    }
}
