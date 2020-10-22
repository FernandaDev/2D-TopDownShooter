using UnityEngine;

public class SimpleUI : MonoBehaviour
{
    [SerializeField] BarUI hpBar;
    IDamageable damageable;

    private void Awake()
    {
        damageable = GetComponentInParent<IDamageable>();
    }

    private void Start()
    {
        damageable.OnHealthChanged += RefreshHp;

        if(hpBar)
            hpBar.SetupValues(damageable.MaxHealth);
    }

    void RefreshHp(int newHp)
    {
        if(hpBar)
            hpBar.RefreshValues((float)newHp);
    }
}
