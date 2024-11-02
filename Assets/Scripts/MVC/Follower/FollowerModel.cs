using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerModel : NPCModel
{
    LeaderModel leader;

    public LeaderModel Leader { get => leader; set => leader = value; }

    private void Update()
    {
        // TODO: Remover
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReceiveDamage(1);
        }
    }
}
