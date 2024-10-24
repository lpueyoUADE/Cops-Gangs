using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityModel : EntityBase, IMove, IPain, IDead
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

    public Transform EyeSight { get => eyeSight; set => eyeSight = value; }
    public float ShieldPoints { get => shieldPoints; set => shieldPoints = value; }
    public float LifePoints { get => lifePoints; set => lifePoints = value; }
    public float Speed { get => speed; set => speed = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float Damage { get => damage; set => damage = value; }

    public virtual void Move(Vector3 dir)
    {
        dir *= Speed;
        dir.y = Rb.velocity.y;
        Rb.velocity = dir;
    }
    public void Look(Vector3 dir)
    {
        dir.y = 0;
        transform.forward = Vector3.RotateTowards(transform.forward, dir, Time.deltaTime * RotationSpeed, 0);
    }
    public void Look(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0;
        Look(dir);
    }
}
