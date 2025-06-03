using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateReload : State<NPCStates>
{
    IMove move;
    IReload reload;
    IFoeDetection foeDetection;

    public NPCStateReload(IMove move, IReload reload, IFoeDetection foeDetection)
    {
        this.move = move;
        this.reload = reload;
        this.foeDetection = foeDetection;
    }
    public override void Enter()
    {
        base.Enter();
        reload.Reload();
    }

    public override void FixedExecute()
    {
        base.FixedExecute();

        if (!foeDetection.IsCurrentTargetSetAndAlive())
        {
            foeDetection.ClearTarget();
            return;
        }

        move.Look(foeDetection.Target.Rb.position);
    }
}
