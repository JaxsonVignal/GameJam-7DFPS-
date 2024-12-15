using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float health = 100f;  // Enemy's health
    public GameObject deathEffect; // Death effect when the enemy is destroyed
    private DroneTracker droneTracker;  // Reference to DroneTracker script

    void Start()
    {
        // Find the DroneTracker object in the scene
        droneTracker = FindObjectOfType<DroneTracker>();
    }

    // Function to take damage
    public void TakeDamage(float damage)
    {
        health -= damage;  // Subtract damage from health

        if (health <= 0f)
        {
            Die();  // If health is 0 or less, call the Die function
        }
    }

    // Function to handle death
    private void Die()
    {
        // Optional: Instantiate death effect
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
        }

        // Notify the DroneTracker that this drone is destroyed
        if (droneTracker != null)
        {
            droneTracker.IncrementDestroyedDrones();  // Increment the destroyed drone count
        }

        // Destroy the enemy object
        Destroy(gameObject);
    }
}