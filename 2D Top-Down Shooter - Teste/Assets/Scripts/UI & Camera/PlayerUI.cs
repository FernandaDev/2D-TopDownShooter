using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] BarUI hpBar;

    PlayerController playerController;
    CharacterHealth playerHealth;
    GunHolder gunHolder;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerHealth = playerController.GetComponent<CharacterHealth>();
        gunHolder = playerController.GetComponent<GunHolder>();
    }

    void Start()
    {
        GameManager.GetInstance().OnScoreChanged += UpdateScoreText;
        gunHolder.OnGunSwitched += SwitchAmmoDisplayReference;
        gunHolder.CurrentGun.OnAmmoChanged += RefreshAmmo;
        playerHealth.OnHealthChanged += RefreshHp;

        if(nameText)
            nameText.text = GameManager.GetInstance().PlayerName;

        if(hpBar)
            hpBar.SetupValues(playerHealth.MaxHealth);
    }

    void RefreshHp(int newHp)
    {
        if(hpBar)
            hpBar.RefreshValues((float)newHp);
    }

    void RefreshAmmo(int newAmmo)
    {
        if(ammoText)
            ammoText.text = $"{newAmmo}/{gunHolder.CurrentGun.GetMaxAmmo}";
    }

    void UpdateScoreText(int newValue)
    {
        if (scoreText)
            scoreText.text = newValue.ToString();
    }

    void SwitchAmmoDisplayReference(Gun oldGun, Gun newGun)
    {
        oldGun.OnAmmoChanged -= RefreshAmmo;
        newGun.OnAmmoChanged += RefreshAmmo;
        
        if(ammoText)
            ammoText.text = $"{newGun.CurrentAmmo}/{newGun.GetMaxAmmo}";
    }

    void OnDisable()
    {
        gunHolder.OnGunSwitched -= SwitchAmmoDisplayReference;
        gunHolder.CurrentGun.OnAmmoChanged -= RefreshAmmo;
        playerHealth.OnHealthChanged -= RefreshHp;
    }
}