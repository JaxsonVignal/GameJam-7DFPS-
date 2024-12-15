using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShoot : MonoBehaviour
{
    public Camera fpsCam;                          // Camera reference to shoot from
    public float damage = 10f;                     // Damage dealt by each shot
    public float range = 100f;                     // Maximum shooting range
    public GameObject bulletImpactEffect;          // Bullet impact particle effect
    public AudioClip shootSound;                   // Sound to play when shooting
    private AudioSource audioSource;                // AudioSource to play the sound

    public float fireRate = 0.1f;                  // Fire rate (shots per second)
    private float nextTimeToFire = 0f;             // Track the next time to shoot

    void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Only allow shooting if enough time has passed since the last shot
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            Shoot();
            // Set the next time the player can shoot based on fireRate
            nextTimeToFire = Time.time + (1f / fireRate);  // Update the next time to fire based on fire rate
        }
    }

    void Shoot()
    {
        RaycastHit hit;  // Raycast hit information

        // Fire a ray from the camera
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            // Instantiate bullet impact effect at the hit point
            GameObject impactEffect = Instantiate(bulletImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            // Destroy the impact effect after 1 second
            Destroy(impactEffect, 1f);

            // Check if we hit an object with the EnemyDamage script (enemy)
            if (hit.collider.GetComponent<EnemyDamage>())
            {
                EnemyDamage enemy = hit.collider.GetComponent<EnemyDamage>();  // Get the EnemyDamage component
                enemy.TakeDamage(damage);  // Apply damage to the enemy
            }

            Debug.Log("Hit: " + hit.collider.name);
        }

        // Debug statement
        Debug.Log("Attempting to play shoot sound...");

        // Play shooting sound if audioSource and shootSound are available
        if (audioSource && shootSound)
        {
            Debug.Log("Playing sound...");
            audioSource.PlayOneShot(shootSound);  // Play the sound on each shot
        }
        else
        {
            Debug.LogError("AudioSource or Shoot Sound not assigned!");
        }
    }
}
