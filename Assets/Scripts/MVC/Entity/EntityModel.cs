using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class EntityModel : EntityBase, IMove, IAttack, IReload, IPain, IDead
{
    [Header("Name")]
    [SerializeField] string entityName;

    [Header("Life")]
    [SerializeField] float maxShieldPoints;
    [SerializeField] float maxLifePoints;

    [Header("Pain")]
    [SerializeField][Range(0, 1f)] float painChance;

    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed = 6;

    [Header("Attack")]
    [SerializeField] float attackRange;
    [SerializeField] Transform attackSpawnPoint;
    [SerializeField] BulletController bullet;
    [SerializeField] float attackCooldownTime;
    [SerializeField] float damage;
    [SerializeField] int maxAmmo;

    [Header("Anim Controller")]
    [SerializeField] EntityAnimController entityAnimController;

    bool isAttacking;
    bool isReloading;
    bool isInPain;

    float acceleration = 35f;

    public float currentShieldPoints;
    public float currentLifePoints;
    int currentAmmo;
    private enum painRouletteEnum
    {
        Pain,
        NoPain
    }

    private Dictionary<painRouletteEnum, float> painRoulette;
    public float MaxShieldPoints { get => maxShieldPoints; set => maxShieldPoints = value; }
    public float MaxLifePoints { get => maxShieldPoints; set => maxShieldPoints = value; }
    public float Speed { get => speed; set => speed = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public float AttackCooldownTime { get => attackCooldownTime; set => attackCooldownTime = value; }
    public float Damage { get => damage; set => damage = value; }
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public bool IsReloading { get => isReloading; set => isReloading = value; }
    public bool IsInPain { get => isInPain; set => isInPain = value; }
    public bool IsDead { get => currentLifePoints <= 0; }
    public bool IsAlive { get => !IsDead; }
    public string EntityName { get => entityName; set { entityName = value; ; OnNameAlteredAction?.Invoke(value); } }
    public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }
    public float CurrentLifePoints { get => currentLifePoints; set { currentLifePoints = value; ; OnStatValueAlteredAction?.Invoke(SliderType.Life, value); } }
    public float CurrentShieldPoints { get => currentShieldPoints; set { currentShieldPoints = value; ; OnStatValueAlteredAction?.Invoke(SliderType.Shield, value); } }
    public int CurrentAmmo { get => currentAmmo; set { currentAmmo = value; ; OnStatValueAlteredAction?.Invoke(SliderType.Ammo, value); } }
    public float AttackRange { get => attackRange; set => attackRange = value; }

    public Action<string> OnNameAlteredAction;
    public Action<SliderType, float> OnStatValueAlteredAction;
    public Action<Sound> OnEmmitSound;
    public Action OnReceivedDamage;

    protected override void Awake()
    {
        base.Awake();

        IsAttacking = false;
        IsReloading = false;

        EntityName = entityName;
        CurrentLifePoints = maxLifePoints;
        CurrentShieldPoints = maxShieldPoints;
        CurrentAmmo = MaxAmmo;

        painRoulette = new()
        {
            { painRouletteEnum.Pain, painChance },
            { painRouletteEnum.NoPain, 1f - painChance }
        };
        entityAnimController.FinishedReloadAction += FinishedReloadActionHandler;
        entityAnimController.FinishedPainAction += FinishedPainActionHandler;
        entityAnimController.StepAction += StepActionHandler;
    }

    private void OnDestroy()
    {
        entityAnimController.FinishedReloadAction -= FinishedReloadActionHandler;
        entityAnimController.FinishedPainAction -= FinishedPainActionHandler;
        entityAnimController.StepAction += StepActionHandler;
    }

    private void FinishedReloadActionHandler()
    {
        OnEmmitSound?.Invoke(Sound.finishReload);
        IsReloading = false;
    }

    private void FinishedPainActionHandler()
    {
        IsInPain = false;
    }

    private void StepActionHandler()
    {
        OnEmmitSound?.Invoke(Sound.step);
    }

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
    public virtual void Look(Vector3 dir)
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
    public void Attack()
    {
        IsAttacking = true;
    }
    public void Shoot()
    {
        OnEmmitSound?.Invoke(Sound.shoot);
        var newBullet = Instantiate(bullet, attackSpawnPoint.position, bullet.transform.rotation);
        newBullet.Direction = transform.forward;
        newBullet.Owner = this.tag;
        newBullet.Damage = this.damage;

        CurrentAmmo--;
    }

    public bool CanAttack()
    {
        return !IsReloading && CurrentAmmo > 0;
    }

    public void Reload()
    {
        OnEmmitSound?.Invoke(Sound.reload);
        IsReloading = true;
        CurrentAmmo = MaxAmmo;
    }

    public bool CanReload()
    {
        return CurrentAmmo != MaxAmmo;
    }

    public bool NeedsToReload()
    {
        return CurrentAmmo == 0;
    }
    /// <summary>
    /// Amount es un valor positivo que indica cuantos puntos se restan al escudo o la vida seg�n corresponda.
    /// </summary>
    /// <param name="amount"></param>
    public virtual void ReceiveDamage(float amount)
    {
        OnReceivedDamage?.Invoke();

        amount = MathF.Abs(amount);

        if (CurrentShieldPoints > 0)
            CurrentShieldPoints = Mathf.Clamp(CurrentShieldPoints - amount, 0, MaxShieldPoints);

        else
            CurrentLifePoints = Mathf.Clamp(CurrentLifePoints - amount, 0, MaxLifePoints);

        if (CurrentLifePoints > 0)
            Pain();

        else
            Die();
    }

    public void ReceiveShield(float amount)
    {
        amount = MathF.Abs(amount);
        CurrentShieldPoints = Mathf.Clamp(CurrentShieldPoints + amount, 0, MaxShieldPoints);
    }

    public void ReceiveLife(float amount)
    {
        amount = MathF.Abs(amount);
        CurrentLifePoints = Mathf.Clamp(CurrentLifePoints + amount, 0, MaxLifePoints);
    }

    public void RefillAmmo()
    {
        CurrentAmmo = MaxAmmo;
    }
    public void Pain()
    {
        if (RandomUtils.Roulette(painRoulette) == painRouletteEnum.Pain)
        {
            OnEmmitSound?.Invoke(Sound.pain);
            IsInPain = true;
        }
    }

    public virtual void Die()
    {
        OnEmmitSound?.Invoke(Sound.dead);
        Bc.enabled = false;
    }

    void IMove.SetPosition(Vector3 position)
    {
        throw new NotImplementedException();
    }
}
