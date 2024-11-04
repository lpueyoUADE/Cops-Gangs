using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerModel : OffensiveNPCModel
{
    LeaderModel leader;

    public LeaderModel Leader { get => leader; set => leader = value; }

    protected override void Update()
    {
        base.Update();
        // TODO: Remover
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReceiveDamage(1);
        }
    }
}
