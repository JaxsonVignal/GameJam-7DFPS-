using UnityEngine;

public class PlayParticleEffectsOnCollision : MonoBehaviour
{
    // Reference to the two particle systems
    public ParticleSystem particleEffect1;
    public ParticleSystem particleEffect2;

    // Reference to the AudioSource for sound
    public AudioSource audioSource;

    // Reference to the sound to be played on collision
    public AudioClip collisionSound;

    // This function will be triggered when the collider enters the trigger area
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding with this object is tagged as "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Play the first particle effect
            if (particleEffect1 != null)
            {
                particleEffect1.Play();
            }

            // Play the second particle effect
            if (particleEffect2 != null)
            {
                particleEffect2.Play();
            }

            // Play the collision sound
            if (audioSource != null && collisionSound != null)
            {
                audioSource.PlayOneShot(collisionSound);
            }
            else
            {
                Debug.LogWarning("AudioSource or collision sound not assigned!");
            }
        }
    }
}
