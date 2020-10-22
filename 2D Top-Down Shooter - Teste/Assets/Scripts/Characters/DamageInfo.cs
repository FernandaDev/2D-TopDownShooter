using UnityEngine;

public struct DamageInfo
{
    public DamageInfo(GameObject attacker, int damageAmount)
    {
        this.attacker = attacker;
        this.damageAmount = damageAmount;
    }

    public GameObject attacker;
    public int damageAmount;
}