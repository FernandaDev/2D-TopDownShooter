using UnityEngine;
using System;

public class Gun : MonoBehaviour, IPooledObject, IDrop, IPickable
{
    public PoolingSystem Pool { get; set; }
    public bool IsDropped { get; private set; }
    public bool IsShooting { get; private set; }
    public int GetMaxAmmo => gunData.maxAmmo;
    public int AmountToPool => (int)ItemRarity;

    int currentAmmo;
    public int CurrentAmmo
    {
        get => currentAmmo;
        private set
        {
            if (currentAmmo != value)
            {
                currentAmmo = Mathf.Clamp(value, 0, GetMaxAmmo); ;
                OnAmmoChanged?.Invoke(currentAmmo);
            }
        }
    }

    [SerializeField] ItemRarity itemRarity;
    public ItemRarity ItemRarity => itemRarity;

    public event Action<int> OnAmmoChanged;

    [SerializeField] GunData gunData;
    [SerializeField] Transform[] shootingPoints;

    float nextTimeToShoot = 0;
    ProjectilePool projectilePool;

    void Awake()
    {
        projectilePool = FindObjectOfType<ProjectilePool>();
    }

    void Start() => CurrentAmmo = gunData.maxAmmo;

    void Update()
    {
        if (IsShooting)
        {
            if (Time.time >= nextTimeToShoot)
            {
                Shoot();
                nextTimeToShoot = Time.time + gunData.fireRate;
            }
        }
    }

    public void StartShooting(bool startShooting)
    {
        if (startShooting && CurrentAmmo <= 0)
            return;

        IsShooting = startShooting;
    }

    public void Shoot()
    {
        for (int i = 0; i < shootingPoints.Length; i++)
        {
            GameObject projectileObject = projectilePool.SpawnObject(gunData.projectile.GetType(),
                                                                     shootingPoints[i].position,
                                                                     shootingPoints[i].rotation);

            Projectile projectile = projectileObject.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectile.Activate(GetComponentInParent<Character>(),
                                    shootingPoints[i].up,
                                    gunData.damage,
                                    gunData.range) ;

                CurrentAmmo--;

                if (CurrentAmmo <= 0)
                    IsShooting = false;
            }
        }
    }

    public bool TryPickupAmmo(int amount)
    {
        if (CurrentAmmo >= gunData.maxAmmo)
            return false;

        CurrentAmmo += amount;

        return true;
    }

    public void Pickup()
    {
        CurrentAmmo = gunData.maxAmmo;
        IsDropped = false;
    }

    public void GetDropped() => IsDropped = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController collidedCharacter = other.GetComponent<PlayerController>();

        if (collidedCharacter != null)
        {
            if (IsDropped)
            {
                collidedCharacter.GunHolder.SwitchGun(this);
                Pickup();
            }
        }
    }
}