using System;
using UnityEngine;

public class AwarenessState : State
{
    public AwarenessState(AIController owner) : base(owner)
    {
        this.owner = owner;
        StateColor = Color.blue;
    }

    public override Type Tick()
    {
        if(!owner.HasTargetAround())
            return typeof(WanderState);
         
        if (owner.IsTargetInSight())
                return typeof(ChaseState);

        SearchForTarget();

        return typeof(AwarenessState);
    }

    private void SearchForTarget()
    {
        owner.CharacterMotor.Move(Vector2.zero);
        owner.CharacterMotor.RotateTowards(owner.GetDirectionToTarget());
    }
}