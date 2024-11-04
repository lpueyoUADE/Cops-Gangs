using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveNPCStateDead : NPCStateDead
{
    Cooldown respawnCooldoown;

    public OffensiveNPCStateDead(IMove move, IDead dead, IRespawn respawn) : base(move, dead)
    {
        respawnCooldoown = new(respawn.RespawnTime, respawn.Respawn);
    }
    public override void Enter()
    {
        base.Enter();
        respawnCooldoown.ResetCooldown();
    }
    public override void Execute()
    {
        base.Execute();
        respawnCooldoown.IsCooldown();
    }
}
