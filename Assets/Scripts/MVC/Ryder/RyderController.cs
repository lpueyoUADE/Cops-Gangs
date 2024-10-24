using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderController : EntityController<PlayerStates>
{
    protected override void InitFSM()
    {
        base.InitFSM();

        fsm = new();

        _move = GetComponent<IMove>();

        var idle = new RyderStateIdle(fsm, _move);
        var run = new RyderStateRun(fsm, _move);
        var attack = new RyderStateAttack(fsm);
        var reload = new RyderStateReload(fsm);
        var pain = new RyderStatePain(fsm);
        var dead = new RyderStateDead(fsm);

        idle.AddTransition(PlayerStates.Run, run);
        idle.AddTransition(PlayerStates.Attack, attack);
        idle.AddTransition(PlayerStates.Reload, reload);
        idle.AddTransition(PlayerStates.Pain, pain);
        idle.AddTransition(PlayerStates.Dead, dead);

        run.AddTransition(PlayerStates.Idle, idle);
        run.AddTransition(PlayerStates.Attack, attack);
        run.AddTransition(PlayerStates.Reload, reload);
        run.AddTransition(PlayerStates.Pain, pain);
        run.AddTransition(PlayerStates.Dead, dead);

        attack.AddTransition(PlayerStates.Idle, idle);
        attack.AddTransition(PlayerStates.Run, run);
        attack.AddTransition(PlayerStates.Reload, reload);
        attack.AddTransition(PlayerStates.Pain, pain);
        attack.AddTransition(PlayerStates.Dead, dead);

        reload.AddTransition(PlayerStates.Idle, idle);
        reload.AddTransition(PlayerStates.Run, run);
        reload.AddTransition(PlayerStates.Attack, attack);
        reload.AddTransition(PlayerStates.Pain, pain);
        reload.AddTransition(PlayerStates.Dead, dead);

        pain.AddTransition(PlayerStates.Idle, idle);
        pain.AddTransition(PlayerStates.Run, run);
        pain.AddTransition(PlayerStates.Attack, attack);
        pain.AddTransition(PlayerStates.Pain, pain);
        pain.AddTransition(PlayerStates.Dead, dead);

        dead.AddTransition(PlayerStates.Idle, idle);
        dead.AddTransition(PlayerStates.Run, run);
        dead.AddTransition(PlayerStates.Attack, attack);
        dead.AddTransition(PlayerStates.Reload, reload);
        dead.AddTransition(PlayerStates.Pain, pain);

        fsm.SetInitial(idle);
    }
}
