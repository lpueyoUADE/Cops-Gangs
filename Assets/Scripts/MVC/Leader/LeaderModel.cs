using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderModel : OffensiveNPCModel
{
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
