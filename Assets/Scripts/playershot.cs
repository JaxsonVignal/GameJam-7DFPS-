using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShoot : MonoBehaviour
{
    public Camera fpsCam;                          // Camera reference to shoot from
    public float damage = 10f;                     // Damage dealt by each shot
    public float range = 100f;                     // Maximum shooting range
    public GameObject bulletImpactEffect;          // Bullet impact particle effect
    public AudioClip shootSound;
    public AudioClip reloadSound;                  // Sound to play when reloading
    private AudioSource audioSource;                // AudioSource to play the sound

    public float fireRate = 0.1f;                  // Fire rate (shots per second)
    private float nextTimeToFire = 0f;             // Track the next time to shoot

    public int maxShots = 10;                      // Max shots before reloading
    public int shotCount = 0;                      // Current shot count
    public float reloadTime = 2f;                  // Time it takes to reload (in seconds)
    public bool isReloading = false;              // Flag to check if the gun is reloading

    void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // If the gun is reloading, skip the shooting logic
        if (isReloading)
        {
            return;
        }

        // Only allow shooting if enough time has passed since the last shot
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            Shoot();
            // Set the next time the player can shoot based on fireRate
            nextTimeToFire = Time.time + (1f / fireRate);  // Update the next time to fire based on fire rate

            // Increment shot count and check if reload is needed
            shotCount++;
            if (shotCount >= maxShots)
            {
                StartCoroutine(Reload());
            }
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
                Debug.Log("Hit enemy: " + hit.collider.name);
            }
            // Check if we hit an object with the RoboEnemy script
            else if (hit.collider.GetComponent<RoboDeath>())
            {
                RoboDeath roboDeath = hit.collider.GetComponent<RoboDeath>();  // Get the RoboDeath component
                roboDeath.TakeDamage(damage);  // Apply damage to the RoboEnemy
                Debug.Log("Hit RoboEnemy: " + hit.collider.name);
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

    // Reload logic with a delay before the reload sound
    IEnumerator Reload()
    {
        isReloading = true;  // Set reloading to true to prevent shooting during reload
        Debug.Log("Reloading...");

        // Wait for the reload to complete before playing the sound
        yield return new WaitForSeconds(reloadTime * 0.2f);  // Half of reload time to delay sound play

        // Play reload sound with a delay
        if (audioSource && reloadSound)
        {
            audioSource.PlayOneShot(reloadSound);  // Play reload sound after the delay
        }
        else
        {
            Debug.LogError("AudioSource or Reload Sound not assigned!");
        }

        // Wait for the remaining reload time
        yield return new WaitForSeconds(reloadTime * 0.5f);  // Complete the reload

        shotCount = 0;  // Reset the shot count after reload
        isReloading = false;  // Set reloading to false, allowing shooting again
        Debug.Log("Reload Complete");
    }
}
