using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderState : State
{
    Vector2 wanderDirection;

    public WanderState(AIController owner) : base(owner)
    {
        this.owner = owner;
        StateColor = Color.cyan;
    }

    public override Type Tick()
    {
        if (!HasDestination())
            GetNewDestination();

        if (owner.HasTargetAround())
        {
            owner.GetClosestTarget();
            owner.SetDestination(Vector3.zero); //Reset Destination
            return typeof(AwarenessState);
        }

        MoveToDestination();

        return typeof(WanderState);
    }

    private void MoveToDestination()
    {
        owner.CharacterMotor.Move(wanderDirection);
        owner.CharacterMotor.RotateTowards(wanderDirection);
    }

    public bool HasDestination()
    {
        if (owner.Destination == Vector3.zero)
            return false;

        float distanceToDestination = Vector2.Distance(ownerTransform.position, owner.Destination);
        if (distanceToDestination <= owner.StoppingThreshold)
            return false;

        return true;
    }

    public void GetNewDestination()
    {
        float randomRadius = Random.Range(owner.MinDestionationRange,
                                          owner.MaxDestionationRange); 

        Vector2 randomPointAround = Random.insideUnitCircle * randomRadius;
        Vector2 desiredDestination = (Vector2)ownerTransform.position + randomPointAround;

        if (Level.GetInstance().CheckIfPointIsInsideMap(desiredDestination) == false) //Flip
        {
            Vector2 flippedDirection = (desiredDestination * -1).normalized;
            desiredDestination = (Vector2)ownerTransform.position + (flippedDirection * randomRadius) ;
        }

        wanderDirection = (desiredDestination - (Vector2)ownerTransform.position).normalized;

        owner.SetDestination(desiredDestination);
    }
}
