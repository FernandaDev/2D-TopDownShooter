using UnityEngine;

public class Ammo : MonoBehaviour, IPooledObject, IDrop, IPickable
{
    public PoolingSystem Pool { get; set; }
    public bool IsDropped { get; private set; }
    public int AmountToPool => (int)ItemRarity;

    [SerializeField] ItemRarity itemRarity;
    public ItemRarity ItemRarity => itemRarity;
    
    [SerializeField] int ammoAmount;

    public void Pickup()
    {
        IsDropped = false;
        Pool.DespawnObject(this.GetType(), this.gameObject);
    }

    public void GetDropped() => IsDropped = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController collidedCharacter = other.GetComponent<PlayerController>();

        if (collidedCharacter != null)
        {
            if (IsDropped)
            {
                if (collidedCharacter.GunHolder.CurrentGun.TryPickupAmmo(ammoAmount))
                    Pickup();
            }
        }
    }
}