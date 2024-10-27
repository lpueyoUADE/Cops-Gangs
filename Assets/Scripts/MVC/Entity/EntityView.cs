using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityView : EntityBase
{
    [Header("Animator")]
    [SerializeField] protected Animator anim;

    [Header("HUD")]
    [SerializeField] private Transform canvas;
    [SerializeField] private Transform HUD; 
    [SerializeField] private Vector3 offset;

    IAttack _attack;
    IReload _reload;
    IPain _pain;
    IDead _dead;

    protected override void Awake()
    {
        base.Awake();
        _attack = GetComponent<IAttack>();
        _reload = GetComponent<IReload>();
        _pain = GetComponent<IPain>();
        _dead = GetComponent<IDead>();
    }

    void Start()
    {
        HUD.transform.SetParent(canvas);
    }

    protected virtual void Update()
    {
        // HUD.transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position); // Look at the camera
        HUD.transform.position = this.transform.position + offset;

        anim.SetFloat("Velocity", new Vector3(Rb.velocity.x, 0, Rb.velocity.z).magnitude);
        anim.SetBool("IsAttacking", _attack.IsAttacking);
        anim.SetBool("IsReloading", _reload.IsReloading);
        anim.SetBool("IsInPain", _pain.IsInPain);
        anim.SetBool("IsDead", _dead.IsDead);
    }
}
