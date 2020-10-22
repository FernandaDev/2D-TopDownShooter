using System;
using UnityEngine;

public abstract class State
{
    public Color StateColor { get; protected set; }
    protected AIController owner;
    protected Transform ownerTransform;

    public State(AIController owner)
    {
        this.owner = owner;
        ownerTransform = owner.transform;
    }

    public abstract Type Tick();
}
