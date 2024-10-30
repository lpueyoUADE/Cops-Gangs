using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownModel : NPCModel
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
