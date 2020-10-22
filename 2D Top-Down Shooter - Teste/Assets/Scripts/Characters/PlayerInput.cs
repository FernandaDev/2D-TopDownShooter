using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    Vector2 moveVector;
    public Vector2 MoveVector => moveVector;

    Vector2 mousePosition;
    public Vector2 MousePosition => mousePosition;

    public event Action<bool> IsShooting;

    [SerializeField] Camera cam;

    private void Awake()
    {
        cam = cam ?? Camera.main;
        if (cam == null)
            cam = FindObjectOfType<Camera>();
    }

    public void Update()
    {
        moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Fire1"))
        {
            IsShooting?.Invoke(true);
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            IsShooting?.Invoke(false);
        }
    }
}
