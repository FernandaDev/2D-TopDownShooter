using System;
using UnityEngine;

public class ShootState : State
{
    Gun CurrentGun => owner.GunHolder.CurrentGun;

    public ShootState(AIController owner) : base(owner)
    {
        this.owner = owner;
        StateColor = Color.red;
    }

    public override Type Tick()
    {
        if (!owner.IsTargetInShootingDistance())
        {
            AimAndShoot(false);
            return typeof(ChaseState);
        }

        AimAndShoot(true);
        
        return typeof(ShootState);
    }

    void AimAndShoot(bool startShooting)
    {
        owner.CharacterMotor.RotateTowards(owner.GetDirectionToTarget());
        owner.CharacterMotor.Move(Vector3.zero);

        if (!CurrentGun.IsShooting && startShooting == true)
            CurrentGun.StartShooting(true);
        else
            CurrentGun.StartShooting(false);
    }
}