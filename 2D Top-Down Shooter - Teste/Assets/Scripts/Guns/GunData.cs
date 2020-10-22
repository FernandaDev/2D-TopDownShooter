using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun Data")]
public class GunData : ScriptableObject
{
    public float range;
    public float fireRate;
    public int damage;
    public int maxAmmo;
    public Projectile projectile; 
}
