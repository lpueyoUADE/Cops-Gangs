using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderStateReload : State<PlayerStates>
{
    FSM<PlayerStates> fsm;
    IReload reload;
    IDead dead;

    public RyderStateReload(FSM<PlayerStates> fsm, IReload reload, IDead dead)
    {
        this.fsm = fsm;
        this.reload = reload;
        this.dead = dead;
    }
    public override void Enter()
    {
        base.Enter();
        reload.Reload();
    }

    public override void FixedExecute()
    {
        base.FixedExecute();
        
        if (dead.IsDead)
        {
            fsm.Transition(PlayerStates.Dead);
            return;
        }

        if (reload.IsReloading)
            return;

        fsm.Transition(PlayerStates.Idle);
    }
}
