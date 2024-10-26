using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class EntityModel : EntityBase, IMove, IAttack, IPain, IDead
{
    [Header("Parameters")]
    [SerializeField] float shieldPoints;
    [SerializeField] float lifePoints;

    [SerializeField] float speed;
    [SerializeField] float rotationSpeed = 6;

    [SerializeField] float attackSpeed;
    [SerializeField] float damage;

    [Header("Eye Sight")]
    [SerializeField] Transform eyeSight;

    private bool isAttacking;
    private float acceleration = 35f;
    public Vector3 velocity = Vector3.zero;

    public Transform EyeSight { get => eyeSight; set => eyeSight = value; }
    public float ShieldPoints { get => shieldPoints; set => shieldPoints = value; }
    public float LifePoints { get => lifePoints; set => lifePoints = value; }
    public float Speed { get => speed; set => speed = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float Damage { get => damage; set => damage = value; }
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }

    protected void Start()
    {
        IsAttacking = false;
    }

    public virtual void Move(Vector3 dir)
    {
        /*dir *= Speed;
        dir.y = Rb.velocity.y;
        Rb.velocity = dir;
        */

        Vector3 smoothedVelocity = Vector3.MoveTowards(Rb.velocity, dir * Speed, acceleration * Time.fixedDeltaTime);
        Rb.velocity = new Vector3(smoothedVelocity.x, 0, smoothedVelocity.z);
    }

    public void Look(Vector3 dir)
    {
        dir = dir - transform.position;
        dir.y = 0;

        transform.forward = Vector3.RotateTowards(transform.forward, dir, Time.deltaTime * RotationSpeed, 0);
    }
    public void Look(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0;
        Look(dir);
    }

    public void Attack()
    {
        IsAttacking = true;
    }

    public bool CanAttack()
    {
        throw new System.NotImplementedException();
    }

}
