using UnityEngine;

public class RoboDeath : MonoBehaviour
{
    public float health = 100f;  // Initial health for the RoboEnemy (changed to float)
    public float damageAmount = 10f;  // Damage amount for each bullet hit (changed to float)
    public GameObject deathEffect;  // Reference to the particle effect prefab for death
    public AudioClip deathSound;  // Reference to the death sound effect

    private AudioSource audioSource;  // AudioSource component reference

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on RoboEnemy!");
        }
    }

    // Method to apply damage to the RoboEnemy
    public void TakeDamage(float damage)  // Changed to float
    {
        health -= damage;  // Decrease health by the damage amount

        // Check if the RoboEnemy's health is zero or below
        if (health <= 0f)
        {
            Die();  // Call the Die method if health reaches zero
        }

        Debug.Log("RoboEnemy Health: " + health);
    }

    // Method to handle RoboEnemy's death
    private void Die()
    {
        // Optionally, play death animation or other effects
        Debug.Log("RoboEnemy has died!");

        // Play the death particle effect (if one is assigned)
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
        }

        // Play death sound effect (if assigned)
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);  // Play the death sound on death
            Debug.Log("Death sound played!");
        }

        // Destroy the RoboEnemy game object after playing the death effect
        Destroy(gameObject);
    }
}


