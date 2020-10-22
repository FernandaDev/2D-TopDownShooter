using UnityEngine;
using System;

public class GunHolder : MonoBehaviour
{
    public Gun CurrentGun;
    [SerializeField] Transform gunContainerTransform;

    public event Action<Gun,Gun> OnGunSwitched;

    private void Awake()
    {
        if (gunContainerTransform == null)
            Debug.LogError("Assign a gun container.");
    }

    public void SwitchGun(Gun newGun)
    {
        if (CurrentGun == null)
            return;

        OnGunSwitched?.Invoke(CurrentGun,newGun);

        if (CurrentGun.Pool != null)
        {
            if (CurrentGun.IsShooting)
                CurrentGun.StartShooting(false); // parar de atirar antes de despawnar.

            CurrentGun.Pool.DespawnObject(CurrentGun.GetType(), CurrentGun.gameObject);
        }
        else
            Destroy(CurrentGun.gameObject); // 'Simple Gun' vai ser destruída, pois ela não "spawna". =(

        newGun.transform.parent = gunContainerTransform;
        newGun.transform.position = gunContainerTransform.position;
        newGun.transform.rotation = gunContainerTransform.rotation;

        CurrentGun = newGun;
    }
}
