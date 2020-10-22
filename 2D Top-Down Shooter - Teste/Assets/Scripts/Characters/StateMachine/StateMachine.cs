using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public Dictionary<Type, State> states { get; private set; }
    public State CurrentState { get; private set; }
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void CreateStates(AIController owner)
    {
        states = new Dictionary<Type, State>
        {
            { typeof(WanderState), new WanderState(owner) },
            { typeof(AwarenessState), new AwarenessState(owner) },
            { typeof(ChaseState), new ChaseState(owner) },
            { typeof(ShootState), new ShootState(owner) }
        };
    }

    private void Update()
    {
        if (CurrentState == null)
            CurrentState = states.Values.First();
        
        Type newStateType = CurrentState.Tick();

        if (newStateType != null && newStateType != CurrentState.GetType())
        {
            SetNewState(newStateType);
        }
    }

    void SetNewState(Type newStateType)
    {
        CurrentState = states[newStateType];
        spriteRenderer.color = CurrentState.StateColor;
    }
}
