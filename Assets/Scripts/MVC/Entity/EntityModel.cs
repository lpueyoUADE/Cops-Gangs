using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class EntityModel : EntityBase, IMove, IAttack, IReload, IPain, IDead
{
    [Header("Life")]
    [SerializeField] float maxShieldPoints;
    [SerializeField] float maxLifePoints;

    [Header("Pain")]
    [SerializeField][Range(0, 1f)] float painChance;

    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed = 6;

    [Header("Attack")]
    [SerializeField] Transform attackSpawnPoint;
    [SerializeField] BulletController bullet;
    [SerializeField] float attackCooldownTime;
    [SerializeField] float damage;
    [SerializeField] int maxAmmo;

    [Header("Eye Sight")]
    [SerializeField] Transform eyeSight;

    private bool isAttacking;
    private bool isReloading;
    private bool isInPain;
    private bool isDead;

    public int currentAmmo;
    private float acceleration = 35f;

    public float currentShieldPoints;
    public float currentLifePoints;

    private enum painRouletteEnum
    {
        Pain,
        NoPain
    }

    private Dictionary<painRouletteEnum, float> painRoulette;
    public Transform EyeSight { get => eyeSight; set => eyeSight = value; }
    public float MaxShieldPoints { get => maxShieldPoints; set => maxShieldPoints = value; }
    public float MaxLifePoints { get => maxShieldPoints; set => maxShieldPoints = value; }
    public float Speed { get => speed; set => speed = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public float AttackCooldownTime { get => attackCooldownTime; set => attackCooldownTime = value; }
    public float Damage { get => damage; set => damage = value; }
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public bool IsReloading { get => isReloading; set => isReloading = value; }
    public bool IsInPain { get => isInPain; set => isInPain = value; }
    public bool IsDead { get => isDead; set => isDead = value; }

    protected void Start()
    {
        IsAttacking = false;
        IsReloading = false;
        currentAmmo = maxAmmo;
        currentShieldPoints = maxShieldPoints;
        currentLifePoints = maxLifePoints;

        painRoulette = new()
        {
            { painRouletteEnum.Pain, painChance },
            { painRouletteEnum.NoPain, 1f - painChance }
        };
    }

    public static Action PainAction;

    private void _Move(Vector3 dir, float movementSpeed)
    {
        Vector3 smoothedVelocity = Vector3.MoveTowards(Rb.velocity, dir * movementSpeed, acceleration * Time.fixedDeltaTime);
        Rb.velocity = new Vector3(smoothedVelocity.x, 0, smoothedVelocity.z);
    }

    public virtual void Move(Vector3 dir)
    {
        _Move(dir, Speed);
    }
    public void MoveSlow(Vector3 dir)
    {
        _Move(dir, Speed / 3);
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

    public void Shoot()
    {
        print("Shoot");
        var newBullet = Instantiate(bullet, attackSpawnPoint.position, bullet.transform.rotation);
        newBullet.Direction = transform.forward;
        newBullet.Owner = this.tag;
        newBullet.Damage = this.damage;

        currentAmmo--;
    }

    public bool CanAttack()
    {
        return !IsReloading && currentAmmo > 0;
    }

    public void Reload()
    {
        IsReloading = true;
        currentAmmo = maxAmmo;
    }

    public bool CanReload()
    {
        return currentAmmo != maxAmmo;
    }

    public bool NeedsToReload()
    {
        return currentAmmo == 0;
    }
    /// <summary>
    /// Amount es un valor positivo que indica cuantos puntos se restan al escudo o la vida según corresponda.
    /// </summary>
    /// <param name="amount"></param>
    public void ReceiveDamage(float amount)
    {
        amount = MathF.Abs(amount);

        if (currentShieldPoints > 0)
            currentShieldPoints = Mathf.Clamp(currentShieldPoints - amount, 0, maxShieldPoints);

        else
            currentLifePoints = Mathf.Clamp(currentLifePoints - amount, 0, MaxLifePoints);

        if (currentLifePoints > 0)
            Pain();

        else
            Die();
    }

    public void ReceiveShield(float amount)
    {
        amount = MathF.Abs(amount);
        currentShieldPoints = Mathf.Clamp(currentShieldPoints + amount, 0, MaxShieldPoints);
    }

    public void ReceiveLife(float amount)
    {
        amount = MathF.Abs(amount);
        currentLifePoints = Mathf.Clamp(currentLifePoints + amount, 0, MaxLifePoints);
    }
    public void Pain()
    {
        if (RandomUtils.Roulette(painRoulette) == painRouletteEnum.Pain)
            IsInPain = true;
    }

    public void Die()
    {
        // TODO: Consultar si es correcto
        /*
        - Es correcto que el model Setee IsInPain, IsAttacking, IsDead? 
	        - Se puede pasar esa lógica a los estados?
	        - Si la lógica es interna a los estados, como consulto si el está en IsInPain, IsAttacking, IsDead?
         */
        IsDead = true;
    }
}
