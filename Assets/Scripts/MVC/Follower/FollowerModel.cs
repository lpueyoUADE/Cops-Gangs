using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerModel : NPCModel
{
    private void Update()
    {
        // TODO: Remover
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReceiveDamage(1);
        }
    }
}
