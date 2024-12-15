using UnityEngine;

public class PlayerStartRotationWithRigidbody : MonoBehaviour
{
    public float startingRotationY = 90f;  // Y-axis rotation angle for the player
    private Rigidbody rb;

    void Start()
    {
        // Ensure the Rigidbody is retrieved
        rb = GetComponent<Rigidbody>();

        // Disable physics updates temporarily to manually set rotation
        rb.isKinematic = true;

        // Set the player's rotation to the desired value (in this case, 90 degrees on the Y-axis)
        transform.rotation = Quaternion.Euler(0, startingRotationY, 0);

        // Re-enable physics updates
        rb.isKinematic = false;
    }
}