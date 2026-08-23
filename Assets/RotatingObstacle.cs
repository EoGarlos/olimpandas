using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 120f;

    private void FixedUpdate()
    {
        transform.Rotate(
            0f,
            0f,
            rotationSpeed * Time.fixedDeltaTime
        );
    }
}