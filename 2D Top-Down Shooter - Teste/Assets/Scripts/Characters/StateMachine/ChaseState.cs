using System;
using UnityEngine;

public class ChaseState : State
{
    public ChaseState(AIController owner) : base(owner)
    {
        this.owner = owner;
        StateColor = Color.yellow;
    }

    public override Type Tick()
    {
        if (!owner.IsTargetInSight())
            return typeof(AwarenessState);

        if (owner.IsTargetInShootingDistance())
            return typeof(ShootState);

        ChaseTarget();

        return typeof(ChaseState);
    }

    void ChaseTarget()
    {
        owner.CharacterMotor.RotateTowards(owner.GetDirectionToTarget());
        owner.CharacterMotor.Move(owner.GetDirectionToTarget());
    }
}
