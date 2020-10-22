using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : Character
{
    PlayerInput playerInput;

    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start() => playerInput.IsShooting += ShootGun;

    private void OnDisable() => playerInput.IsShooting -= ShootGun;

    private void FixedUpdate()
    {
        Vector2 mouseDirection = (playerInput.MousePosition - (Vector2)transform.position).normalized;
        CharacterMotor.LookAt(mouseDirection);

        CharacterMotor.Move(playerInput.MoveVector);
    }
}