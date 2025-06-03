using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderStateAttack : State<PlayerStates>
{
    FSM<PlayerStates> fsm;
    IMoveMouse moveMouse;
    IAttack attack;
    IReload reload;
    IPain pain;
    IDead dead;

    Cooldown attackCoolDown;

    public RyderStateAttack(FSM<PlayerStates> fsm, IMoveMouse moveMouse, IAttack attack, IReload reload, IPain pain, IDead dead)
    {
        this.fsm = fsm;
        this.moveMouse = moveMouse;
        this.attack = attack;
        this.reload = reload;
        this.pain = pain;
        this.dead = dead;

        attackCoolDown = new(this.attack.AttackCooldownTime);
    }
    public override void Enter()
    {
        base.Enter();
        attack.Attack();
    }

    public override void Execute()
    {
        base.Execute();

        if (dead.IsDead)
        {
            fsm.Transition(PlayerStates.Dead);
            return;
        }

        moveMouse.LookAround();

        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v).normalized;

        moveMouse.MoveSlow(dir);

        if (!Input.GetMouseButton(0))
        {
            attack.IsAttacking = false;
            fsm.Transition(PlayerStates.Idle);
        }

        attackCoolDown.RunCooldown();

        if (!attackCoolDown.IsCooldown())
        {
            attack.Shoot();
            attackCoolDown.ResetCooldown();
        }

        if(reload.NeedsToReload() || (Input.GetKey(KeyCode.R) && reload.CanReload()))
            fsm.Transition(PlayerStates.Reload);

        if (pain.IsInPain)
            fsm.Transition(PlayerStates.Pain);
    }

    public override void Exit()
    {
        base.Exit();
        attack.IsAttacking = false;
    }
}
