using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderModel : OffensiveNPCModel
{
    [Header("Patrol")]
    [SerializeField] Node _start;
    public Node Start { get { return _start; } }
    [SerializeField] Node _goal;
    public Node Goal { get { return _goal; } }

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
