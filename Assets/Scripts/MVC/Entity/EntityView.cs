using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityView : EntityBase
{
    [SerializeField] protected Animator anim;

    void Update()
    {
        anim.SetFloat("Velocity", new Vector3(Rb.velocity.x, 0, Rb.velocity.z).magnitude);
    }
}
