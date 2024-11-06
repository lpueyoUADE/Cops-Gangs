using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveNPCStateDead : NPCStateDead
{
    Cooldown respawnCooldoown;
    Transform tranform;
    IFoeDetection foeDetection;
    IReload reload;

    public OffensiveNPCStateDead(IMove move, IFoeDetection foeDetection, IReload reload, IRespawn respawn, Transform transform) : base(move)
    {
        respawnCooldoown = new(respawn.RespawnTime, respawn.Respawn);
        this.tranform = transform;
        this.foeDetection = foeDetection;
        this.reload = reload;
    }
    public override void Enter()
    {
        base.Enter();
        respawnCooldoown.ResetCooldown();
        if(tranform.gameObject.layer == LayerMask.NameToLayer("Police"))
            GameManager.Instance.InstatiateRollDynamicItem(tranform.position);
        foeDetection.ClearTarget();
        reload.IsReloading = false;
    }
    public override void Execute()
    {
        base.Execute();
        respawnCooldoown.IsCooldown();
    }
}
