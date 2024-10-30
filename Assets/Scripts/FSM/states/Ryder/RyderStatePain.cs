using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderStatePain : State<PlayerStates>
{
    FSM<PlayerStates> fsm;
    IMoveMouse moveMouse;
    IPain pain;
    IDead dead;
    public RyderStatePain(FSM<PlayerStates> fsm, IMoveMouse moveMouse, IPain pain, IDead dead)
    {
        this.fsm = fsm;
        this.moveMouse = moveMouse;
        this.pain = pain;
        this.dead = dead;
    }
    public override void Enter()
    {
        base.Enter();
        moveMouse.Move(Vector3.zero);
    }

    public override void Execute()
    {
        base.Execute();

        if (pain.IsInPain)
            return;

        if (dead.IsDead)
        {
            fsm.Transition(PlayerStates.Dead);
            return;
        }

        fsm.Transition(PlayerStates.Idle);
    }
}
