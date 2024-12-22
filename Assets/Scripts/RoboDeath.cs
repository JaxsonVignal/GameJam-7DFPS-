using UnityEngine;
using System.Collections;

public class RoboDeath : MonoBehaviour
{
    public float health = 100f;  // Initial health for the RoboEnemy
    public float damageAmount = 10f;  // Damage amount for each bullet hit
    public GameObject deathEffect;  // Reference to the particle effect prefab for death
    public AudioClip deathSound;  // Death sound clip to be played on death
    private AudioSource audioSource;  // Reference to the AudioSource component
    Rigidbody rb;
    void Start()
    {
        // Ensure the AudioSource component is attached to the GameObject
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        // If no AudioSource component is found, add one and log a warning
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.LogWarning("AudioSource component was not found. Adding one.");
        }

        // Set the AudioSource to not loop and ensure it's ready to play sounds.
        audioSource.loop = false;
    }

    // Method to apply damage to the RoboEnemy
    public void TakeDamage(float damage)
    {
        health -= damage;  // Subtract damage from health

        if (health <= 0f)
        {
            Die();  // If health is 0 or less, call the Die function
        }

        Debug.Log("RoboEnemy Health: " + health);
    }

    // Method to handle RoboEnemy's death
    private void Die()
    {
        Debug.Log("RoboEnemy has died!");

        // Optionally, instantiate the death effect
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
        }

        // Play the death sound if it's assigned
        if (audioSource != null && deathSound != null)
        {
            Debug.Log("Playing death sound");
            audioSource.PlayOneShot(deathSound);  // Play the death sound effect
            // Start the coroutine to wait for the sound to finish before destroying the enemy
            StartCoroutine(WaitForSoundAndDestroy());
        }
        else
        {
            Debug.LogError("AudioSource or Death Sound is missing. Check setup.");
            // If no sound is found, immediately destroy the enemy
            Destroy(gameObject);
        }
    }

    // Coroutine to wait for the death sound to finish playing before destroying the object
    private IEnumerator WaitForSoundAndDestroy()
    {
        rb.velocity = Vector3.zero;
        // Wait until the sound has finished playing (based on the length of the sound clip)
        yield return new WaitForSeconds(deathSound.length - 1.7f);

        // Destroy the RoboEnemy after the sound has finished
        Destroy(gameObject);
    }
}
