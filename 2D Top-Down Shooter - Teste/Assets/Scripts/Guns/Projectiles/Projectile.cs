using UnityEngine;

public abstract class Projectile: MonoBehaviour, IPooledObject
{
    public PoolingSystem Pool { get; set; }

    [SerializeField] protected int amountToPool;
    public int AmountToPool => amountToPool;

    [SerializeField] protected float moveSpeed;

    protected Character shooter;
    protected Vector2 direction;
    protected Vector2 startPosition;
    protected int damage;
    protected float range;
    protected float distanceTraveled;
    protected bool isActive;

    protected void Update()
    {
        if (isActive)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            
            distanceTraveled = Vector2.Distance(transform.position, startPosition);

            if (distanceTraveled >= range)
                Despawn();
        }
    }

    public void Activate(Character shooter, Vector3 direction, int damage, float range)
    {
        this.shooter = shooter;
        this.direction = direction;
        this.damage = damage;
        this.range = range;

        startPosition = transform.position;
        distanceTraveled = 0;
        isActive = true;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponentInParent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(new DamageInfo(shooter.gameObject, damage));
            Despawn();
        }
    }

    protected void Despawn()
    {
        isActive = false;
        Pool.DespawnObject(this.GetType(), this.gameObject);
    }
}