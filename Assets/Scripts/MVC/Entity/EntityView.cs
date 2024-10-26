using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityView : EntityBase
{
    [SerializeField] protected Animator anim;

    IAttack _attack;

    protected override void Awake()
    {
        base.Awake();
        _attack = GetComponent<IAttack>();
    }

    protected virtual void Update()
    {
        anim.SetFloat("Velocity", new Vector3(Rb.velocity.x, 0, Rb.velocity.z).magnitude);
        anim.SetBool("IsAttacking", _attack.IsAttacking);
    }
}
