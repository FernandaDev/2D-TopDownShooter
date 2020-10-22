using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth;
    public int MaxHealth => maxHealth;

    [SerializeField] int damageOnCollision = 5;

    public static event Action<CharacterHealth> OnDeath;
    public event Action<int> OnHealthChanged;

    int currentHealth;
    public int CurrentHealth
    {
        get => currentHealth;
        private set
        {
            currentHealth = value;
            OnHealthChanged?.Invoke(value);
        }
    }

    private void Start() => CurrentHealth = MaxHealth;

    public void TakeDamage(DamageInfo damageInfo)
    {
        CurrentHealth -= damageInfo.damageAmount;

        if (CurrentHealth <= 0)
            Die(damageInfo);
    }

    public void Die(DamageInfo damageInfo)
    {
        gameObject.SetActive(false);
        gameObject.transform.position = Vector3.one * 1000f;
        OnDeath?.Invoke(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        IDamageable otherDamageable = other.collider.GetComponentInParent<IDamageable>();

        if (otherDamageable != null)
        {
            otherDamageable.TakeDamage(new DamageInfo(this.gameObject, damageOnCollision));
            TakeDamage(new DamageInfo(other.gameObject, damageOnCollision));
        }
    }
}
