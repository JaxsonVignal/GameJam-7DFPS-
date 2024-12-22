using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float health = 100f;  // Enemy's health
    public GameObject deathEffect; // Death effect when the enemy is destroyed
    public AudioClip deathSound;  // Death sound clip to be played on death
    private AudioSource audioSource;  // Reference to the AudioSource component
    private DroneTracker droneTracker;  // Reference to DroneTracker script

    void Start()
    {
        // Find the DroneTracker object in the scene
        droneTracker = FindObjectOfType<DroneTracker>();

        // Ensure the AudioSource component is attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // If no AudioSource component is found, add one and log a warning
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.LogWarning("AudioSource component was not found. Adding one.");
        }

        // Set the AudioSource to not loop and ensure it's ready to play sounds.
        audioSource.loop = false;
    }

    // Function to take damage
    public void TakeDamage(float damage)
    {
        health -= damage;  // Subtract damage from health
        Debug.Log("Health after damage: " + health);

        if (health <= 0f)
        {
            Debug.Log("Health is 0 or less. Triggering Die function.");
            Die();  // If health is 0 or less, call the Die function
        }
    }

    // Function to handle death
    private void Die()
    {
        Debug.Log("Die() function called");

        // Optional: Instantiate death effect (e.g., explosion or particle system)
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Debug.Log("Death effect instantiated.");
        }

        // Play the death sound if it's assigned
        if (audioSource != null && deathSound != null)
        {
            Debug.Log("Playing death sound");
            audioSource.PlayOneShot(deathSound);  // Play the death sound effect
        }
        else
        {
            Debug.LogError("AudioSource or Death Sound is missing. Check setup.");
        }

        // Notify the DroneTracker that this drone is destroyed
        if (droneTracker != null)
        {
            droneTracker.IncrementDestroyedDrones();  // Increment the destroyed drone count
        }

        // Delay before destroying the object, to allow time for the sound to play
        StartCoroutine(WaitForSoundToPlay());
    }

    // Coroutine to wait for the death sound to finish before destroying the object
    private IEnumerator WaitForSoundToPlay()
    {
        // Wait for the length of the death sound clip before destroying the object
        if (deathSound != null)
        {
            yield return new WaitForSeconds(deathSound.length - 1.8f);
            Destroy(gameObject);
        }
        else
        {
            // If there's no death sound, destroy immediately
            Destroy(gameObject);
        }
    }
}

