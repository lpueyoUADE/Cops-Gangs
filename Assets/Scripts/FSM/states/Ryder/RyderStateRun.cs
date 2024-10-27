using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderStateRun : State<PlayerStates>
{
    FSM<PlayerStates> fsm;
    IMoveMouse moveMouse;
    IReload reload;
    IPain pain;
    IDead dead;

    public RyderStateRun(FSM<PlayerStates> fsm, IMoveMouse moveMouse, IReload reload, IPain pain, IDead dead)
    {
        this.fsm = fsm;
        this.moveMouse = moveMouse;
        this.reload = reload;
        this.pain = pain;
        this.dead = dead;
    }
    public override void Execute()
    {
        base.Execute();
        moveMouse.LookAround();
    }
    public override void FixedExecute()
    {
        base.FixedExecute();
        
        if (dead.IsDead)
        {
            fsm.Transition(PlayerStates.Dead);
            return;
        }

        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        if (h == 0 && v == 0)
        {
            fsm.Transition(PlayerStates.Idle);
        }
        else
        {
            Vector3 dir = new Vector3(h, 0, v).normalized;

            moveMouse.Move(dir);

            if (Input.GetMouseButton(0))
            {
                fsm.Transition(PlayerStates.Attack);
            }

            if (reload.NeedsToReload() || (Input.GetKey(KeyCode.R) && reload.CanReload()))
                fsm.Transition(PlayerStates.Reload);

            if (pain.IsInPain)
                fsm.Transition(PlayerStates.Pain);
        }
    }
}
