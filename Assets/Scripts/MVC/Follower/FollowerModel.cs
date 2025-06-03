using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerModel : OffensiveNPCModel
{
    [Header("Flocking Leader")]
    [SerializeField] EntityModel leader;

    public EntityModel Leader { get => leader; set => leader = value; }

    public bool IsLeaderInSight()
    {
        return Leader != null && (lineOfSight.InSight(Leader.transform) || IsInPersonalSpaceRange(Leader.transform));
    }

    protected override void Awake()
    {
        base.Awake();
    }
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
