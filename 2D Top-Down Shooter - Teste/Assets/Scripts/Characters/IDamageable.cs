using System;

public interface IDamageable
{
    int MaxHealth { get; }
    event Action<int> OnHealthChanged;
    void TakeDamage(DamageInfo damageInfo);
    void Die(DamageInfo damageInfo);
}