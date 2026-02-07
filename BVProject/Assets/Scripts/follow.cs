using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // Player
    public Vector3 offset;            // Distance + angle
    public float smoothSpeed = 5f;    // Camera smoothness

    void Update()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.LookAt(target);
    }
}
