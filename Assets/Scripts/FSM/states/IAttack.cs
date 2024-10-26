public interface IAttack
{
    void Attack();

    bool CanAttack();

    bool IsAttacking { get; set; }
}