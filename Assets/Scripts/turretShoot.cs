using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretShoot : MonoBehaviour
{
    public Transform player; // player
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float fireRate = 0.2f;
    public int burstCount = 3;
    public float burstCooldown = 1.5f;
    public float maxShootingDistance = 30f;
    public float visionAngle = 45f;
    public LayerMask obstacleMask;
    private float nextBurstTime = 0f;
    private int bulletsFiredInBurst = 0;
    private bool isBursting = false;

    void Update()
    {
        if (CanShootPlayer() && Time.time >= nextBurstTime)
        {
            if (!isBursting)
            {
                StartCoroutine(BurstFire());
            }
        }

        // Continuously look at the player
        if (CanShootPlayer())
        {
            RotateTowardsPlayer();
        }
    }

    bool CanShootPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > maxShootingDistance) return false;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer > visionAngle / 2) return false;

        if (Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask)) return false;

        return true;
    }

    void RotateTowardsPlayer()
    {
        Vector3 predictedPosition = PredictPlayerPosition();
        Vector3 direction = (predictedPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    IEnumerator BurstFire()
    {
        isBursting = true;

        while (bulletsFiredInBurst < burstCount)
        {
            Shoot();
            bulletsFiredInBurst++;
            yield return new WaitForSeconds(fireRate); // Delay between bullets in a burst
        }

        bulletsFiredInBurst = 0;
        isBursting = false;
        nextBurstTime = Time.time + burstCooldown; // Delay before the next burst
    }

    void Shoot()
    {
        Vector3 predictedPosition = PredictPlayerPosition();
        Vector3 direction = (predictedPosition - firePoint.position).normalized;

        // Instantiate and fire the projectile
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.SetDirection(direction);
    }

    Vector3 PredictPlayerPosition()
    {
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        Vector3 playerVelocity = playerRb.velocity;
        float distance = Vector3.Distance(firePoint.position, player.position);
        float timeToHit = distance / projectilePrefab.GetComponent<Projectile>().speed;
        Vector3 predictedPosition = player.position + playerVelocity * timeToHit;
        predictedPosition.y += .1f;
        return predictedPosition;
    }
}
