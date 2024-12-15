using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolWithRotation : MonoBehaviour
{
    public Transform pointA; // First patrol point
    public Transform pointB; // Second patrol point
    public float speed = 3f; // Speed at which the enemy moves
    public float rotationSpeed = 5f; // Speed at which the enemy rotates
    private Vector3 targetPosition; // Current target position
    private bool movingToB = true; // Direction flag

    void Start()
    {
        // Initialize target position to pointA or pointB
        targetPosition = pointA.position;
    }

    void Update()
    {
        PatrolMovement();
        RotateTowardsTarget();
    }

    void PatrolMovement()
    {
        // Move towards the target position (pointA or pointB)
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // If the enemy reaches the target position, switch to the other point and rotate 180 degrees
        if (transform.position == targetPosition)
        {
            if (movingToB)
            {
                targetPosition = pointB.position; // Set target to pointB
                movingToB = false; // Change direction
            }
            else
            {
                targetPosition = pointA.position; // Set target to pointA
                movingToB = true; // Change direction
            }
        }
    }

    void RotateTowardsTarget()
    {
        // Calculate the direction towards the target position
        Vector3 targetDirection = targetPosition - transform.position;
        targetDirection.y = 0; // Ignore the y-axis for rotation (no vertical rotation)

        // Rotate the enemy to face the target direction smoothly
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
