using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateAttack : State<NPCStates>
{
    IMoveNPC moveNPC;
    IAttack attack;
    IFoeDetection foeDetection;
    Cooldown attackCoolDown;

    public NPCStateAttack(IMoveNPC moveNPC, IAttack attack, IFoeDetection foeDetection)
    {
        this.moveNPC = moveNPC;
        this.attack = attack;
        this.foeDetection = foeDetection;
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

        if (!foeDetection.IsCurrentTargetSetAndAlive())
        {
            foeDetection.ClearTarget();
            return;
        }

        moveNPC.AimAhead(foeDetection.Target.Rb);

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
