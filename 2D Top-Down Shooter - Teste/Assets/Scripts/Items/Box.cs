using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Box : MonoBehaviour, IDamageable, IGiveReward, IPooledObject
{
    public PoolingSystem Pool { get; set; }

    [SerializeField] int maxHealth;
    public int MaxHealth => maxHealth;

    [SerializeField] int scorePointsToGive;
    public int ScorePointsToGive => scorePointsToGive;

    [Range(0, 100), SerializeField] int chanceToDropLoot;
    public int ChanceToDropLoot => chanceToDropLoot;

    [SerializeField] GameObject[] droppableObejcts;
    public GameObject[] DroppableItems => droppableObejcts;

    [SerializeField] int amountToPool;
    public int AmountToPool => amountToPool;

    int currentHealth;
    public int CurrentHealth
    {
        get => currentHealth;
        private set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                OnHealthChanged?.Invoke(currentHealth);
            }
        }
    }

    public event Action<int> OnHealthChanged;

    DropsPool dropsPool;

    private void Awake() => dropsPool = FindObjectOfType<DropsPool>();

    private void Start() => CurrentHealth = MaxHealth;

    public void TakeDamage(DamageInfo damageInfo)
    {
        CurrentHealth -= damageInfo.damageAmount;

        if (CurrentHealth <= 0)
            Die(damageInfo);
    }

    public void Die(DamageInfo damageInfo)
    {
        if (damageInfo.attacker.GetComponent<PlayerController>())
            GameManager.GetInstance().AddScore(scorePointsToGive);

        if (ShouldDropLoot())
        {
            GameObject dropObject = dropsPool.SpawnRandomDrop(this.transform.position, 
                                                        Quaternion.identity);

            if (dropObject != null)
                dropObject.GetComponent<IDrop>().GetDropped();
        }

        GetDespawned();
    }

    private void GetDespawned()
    {
        CurrentHealth = MaxHealth; // Reseta HP para o respawn.
        Pool.DespawnObject(this.GetType(), this.gameObject);
    }

    public bool ShouldDropLoot()
    {
        int randomNumber = Random.Range(0, 100);

        if (randomNumber <= chanceToDropLoot)
            return true;

        return false;
    }
}