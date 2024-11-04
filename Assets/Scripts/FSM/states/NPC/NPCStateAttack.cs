using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NPCStateAttack : State<NPCStates>
{
    IMove move;
    IAttack attack;
    IFoeDetection foeDetection;
    Cooldown attackCoolDown;
    Transform entity;
    float timePrediction;
    public NPCStateAttack(IMove move, IAttack attack, IFoeDetection foeDetection, Transform entity, float timePrediction)
    {
        this.move = move;
        this.attack = attack;
        this.foeDetection = foeDetection;
        this.entity = entity;
        this.timePrediction = timePrediction;

        attackCoolDown = new(this.attack.AttackCooldownTime);
    }
    public override void Enter()
    {
        base.Enter();
        attack.Attack();
        move.Move(Vector3.zero);
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

        move.Look(Pursuit.GetDir(entity, foeDetection.Target.Rb, timePrediction));

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
