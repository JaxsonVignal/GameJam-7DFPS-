using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;  // The global position to follow
    public float bobbingSpeed = 0.18f;  // Speed of the bobbing
    public float bobbingAmount = 0.05f;  // How much the camera moves up and down
    public float speedThreshold = 2.0f;  // The speed threshold above which headbobbing will occur

    private float timer = 0f;  // Timer to control the bobbing frequency
    private Rigidbody rb;  // The Rigidbody of the Player
    public PlayerMovement pm;
    private void Start()
    {
        // Initialize references
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();

    }

    private void Update()
    {
        // Move the camera to follow the cameraPosition (global movement)
        transform.position = cameraPosition.position;

        // Apply headbobbing only if the Rigidbody's velocity is greater than the threshold
        if (rb.velocity.magnitude > speedThreshold)
        {
            HandleHeadbob();
        }
    }

    private void HandleHeadbob()
    {
        // Check if the player is moving by looking at the Rigidbody's velocity
        if (rb.velocity.magnitude > 0.1f && !pm.sliding)  // If the player is moving
        {
            // Increment the timer to progress the sine wave for headbobbing
            timer += Time.deltaTime * bobbingSpeed;

            // Calculate the bobbing effect using a sine function for smooth movement
            float bobbingY = Mathf.Sin(timer) * bobbingAmount;

            // Apply the bobbing effect to the camera's local position
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + bobbingY, transform.localPosition.z);
        }
        else
        {
            // If the player stops moving, reset the timer and stop the bobbing effect
            timer = 0f;
        }
    }
}

