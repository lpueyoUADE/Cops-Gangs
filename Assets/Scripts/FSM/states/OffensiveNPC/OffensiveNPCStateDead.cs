using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveNPCStateDead : NPCStateDead
{
    Cooldown respawnCooldoown;
    Transform tranform;
    IFoeDetection foeDetection;

    public OffensiveNPCStateDead(IMove move, IFoeDetection foeDetection ,IDead dead, IRespawn respawn, Transform transform) : base(move, dead)
    {
        respawnCooldoown = new(respawn.RespawnTime, respawn.Respawn);
        this.tranform = transform;
        this.foeDetection = foeDetection;
    }
    public override void Enter()
    {
        base.Enter();
        respawnCooldoown.ResetCooldown();
        GameManager.Instance.InstatiateRollDynamicItem(tranform.position);
        foeDetection.ClearTarget();
    }
    public override void Execute()
    {
        base.Execute();
        respawnCooldoown.IsCooldown();
    }
}
