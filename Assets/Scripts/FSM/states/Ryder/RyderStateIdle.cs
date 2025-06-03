using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderStateIdle : State<PlayerStates>
{
    FSM<PlayerStates> fsm;
    IMoveMouse moveMouse;
    IReload reload;
    IPain pain;
    IDead dead;

    public RyderStateIdle(FSM<PlayerStates> fsm, IMoveMouse moveMouse, IReload reload, IPain pain, IDead dead)
    {
        this.fsm = fsm;
        this.moveMouse = moveMouse;
        this.reload = reload;
        this.pain = pain;
        this.dead = dead;
    }
    public override void Enter()
    {
        base.Enter();
        moveMouse.Move(Vector3.zero);
    }

    public override void FixedExecute()
    {
        base.FixedExecute();

        if (dead.IsDead)
        { 
            fsm.Transition(PlayerStates.Dead);
            return;
        }

        moveMouse.LookAround();

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
            fsm.Transition(PlayerStates.Run);

        if(Input.GetMouseButton(0))
            fsm.Transition(PlayerStates.Attack);

        if (reload.NeedsToReload() || (Input.GetKey(KeyCode.R) && reload.CanReload()))
            fsm.Transition(PlayerStates.Reload);

        if (pain.IsInPain)
            fsm.Transition(PlayerStates.Pain);
    }
}
