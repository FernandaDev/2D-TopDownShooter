using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class Character : MonoBehaviour
{
    public CharacterMotor CharacterMotor { get; private set; }
    public GunHolder GunHolder { get; private set; }

    protected virtual void Awake()
    {
        CharacterMotor = GetComponent<CharacterMotor>();
        GunHolder = GetComponent<GunHolder>();
    }

    protected void ShootGun(bool isShooting)
    {
        GunHolder.CurrentGun.StartShooting(isShooting);
    }
}
