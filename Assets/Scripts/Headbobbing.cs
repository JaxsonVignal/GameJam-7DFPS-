using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbobbing : MonoBehaviour
{
    [Header("Settings")]

    [Range(0.001f, 0.01f)]
    public float amount = 0.002f;  // How much the camera moves up and down

    [Range(1f, 30f)]
    public float frequency = 10.0f;  // Speed of the bobbing

    [Range(10f, 100f)]
    public float smooth = 10.0f;  // Smoothing of the bobbing

    [Header("Movement Settings")]
    public float speedThreshold = 2.0f;  // Speed threshold to start headbobbing
    private Rigidbody rb;  // Rigidbody reference for player speed

    void Start()
    {
        // Get the Rigidbody component of the player (assuming this script is attached to the camera)
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Only apply headbobbing if the player's speed exceeds the threshold
        if (rb.velocity.magnitude > speedThreshold)
        {
            CheckHeadbob();
        }
    }

    private void CheckHeadbob()
    {
        float inputMagnitude = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).magnitude;

        if (inputMagnitude > 0)  // Check if there is any input (i.e., player is moving)
        {
            StartHeadbob();
        }
    }

    private Vector3 StartHeadbob()
    {
        Vector3 pos = Vector3.zero;

        // Apply headbobbing effect using sine and cosine for smooth movement
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amount * 1.4f, smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * frequency / 2f) * amount * 1.4f, smooth * Time.deltaTime);

        // Apply the calculated bobbing to the camera's local position
        transform.localPosition += pos;

        return pos;
    }
}
