using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateReload : State<FollowerStates>
{
    IReload reload;

    public NPCStateReload(IReload reload)
    {
        this.reload = reload;
    }
    public override void Enter()
    {
        base.Enter();
        reload.Reload();
    }

    public override void FixedExecute()
    {
        base.FixedExecute();
    }
}
