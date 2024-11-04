using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveNPCView : NPCView
{
    IAttack _attack;
    IReload _reload;
    protected override void Awake()
    {
        base.Awake();
        _attack = GetComponent<IAttack>();
        _reload = GetComponent<IReload>();
    }

    protected override void Update()
    {
        base.Update();
        anim.SetBool("IsAttacking", _attack.IsAttacking);
        anim.SetBool("IsReloading", _reload.IsReloading);
    }
}
