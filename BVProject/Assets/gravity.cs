using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SimpleGravity : MonoBehaviour
{
    public float gravity = 20f;
    public float maxFallSpeed = 25f;
    public float groundCheckDistance = 0.05f;
    public LayerMask groundLayer;

    private float verticalVelocity;
    private bool isGrounded;
    private Collider col;

    void Start()
    {
        col = GetComponentInChildren<Collider>();
    }

    void Update()
    {
        CheckGround();

        if (isGrounded)
        {
            if (verticalVelocity < 0)
                verticalVelocity = 0f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;

            // Clamp fall speed (VERY IMPORTANT)
            verticalVelocity = Mathf.Max(verticalVelocity, -maxFallSpeed);

            transform.position += Vector3.up * verticalVelocity * Time.deltaTime;
        }
    }

    void CheckGround()
    {
        Vector3 bottom = col.bounds.center;
        bottom.y = col.bounds.min.y;

        isGrounded = Physics.Raycast(bottom, Vector3.down, groundCheckDistance, groundLayer);
    }
}