public interface IAttack
{
    void Attack();

    void Shoot();
    bool CanAttack();

    bool IsAttacking { get; set; }

    float AttackCooldownTime { get; set; }
}