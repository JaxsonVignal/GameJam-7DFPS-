using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the object
    private float currentHealth;    // Current health of the object

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Set the current health to the max health at the start
    }

    // Method to apply damage to the object
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;  // Subtract the damage from the current health

        // Check if the object has died
        if (currentHealth <= 0f)
        {
            Die();  // Call the method to destroy the object
        }
    }

    // Method to handle destruction
    private void Die()
    {
        // Destroy the game object
        Destroy(gameObject);
    }

    // Optional: method to get the current health (useful for debugging or UI)
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}