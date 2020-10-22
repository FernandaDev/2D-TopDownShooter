using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMotor : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float rotateSpeed = 4f;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void RotateTowards(Vector2 direction)
    {
        Quaternion directionRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.RotateTowards(this.transform.rotation,
                                                      directionRotation,
                                                      rotateSpeed);
    }

    public void LookAt(Vector2 direction)
    {
        this.transform.up = direction;
    }

    public void Move(Vector2 moveVector)
    {
        rb.velocity = moveVector * moveSpeed;
    }
}