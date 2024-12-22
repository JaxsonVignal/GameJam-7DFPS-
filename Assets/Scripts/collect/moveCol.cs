using UnityEngine;

public class RotateAndBounce : MonoBehaviour
{
    public float rotationSpeed = 50f;  // Speed of rotation (degrees per second)
    public float bounceHeight = 0.5f;  // Height of the bounce
    public float bounceSpeed = 2f;  // Speed of the bounce (how fast it goes up and down)

    private Vector3 startPosition;  // Starting position of the object

    void Start()
    {
        // Save the initial position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotate the object around its Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Apply the bouncing effect
        float newY = startPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
