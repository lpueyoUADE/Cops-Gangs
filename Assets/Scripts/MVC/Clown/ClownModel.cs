using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownModel : NPCModel
{
    protected override void Awake()
    {
        base.Awake();
        SetTarget(FindAnyObjectByType<RyderModel>());
    }
    private void Update()
    {
        // TODO: Remover
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReceiveDamage(1);
        }
    }
}
