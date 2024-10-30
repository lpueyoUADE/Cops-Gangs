using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateAttack : State<FollowerStates>
{
    IAttack attack;
    Cooldown attackCoolDown;

    public NPCStateAttack(IAttack attack)
    {
        this.attack = attack;
        attackCoolDown = new(this.attack.AttackCooldownTime);
    }
    public override void Enter()
    {
        base.Enter();
        attack.Attack();
    }

    public override void FixedExecute()
    {
        base.FixedExecute();

        attackCoolDown.RunCooldown();

        if (!attackCoolDown.IsCooldown())
        {
            attack.Shoot();
            attackCoolDown.ResetCooldown();
        }
    }

    public override void Exit()
    {
        base.Exit();
        attack.IsAttacking = false;
    }
}
