using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OffensiveNPCController<T> : NPCController<T> where T : Enum
{
    protected IRespawn _respawn;
    protected override void Awake()
    {
        base.Awake();
        _respawn = GetComponent<IRespawn>();
    }
}
