using System.Collections;
using UnityEngine;

public class EnemyPatrolAndExplode : MonoBehaviour
{
    public Transform patrolPoint1;  // First patrol point
    public Transform patrolPoint2;  // Second patrol point
    public float patrolSpeed = 2f;  // Patrol movement speed
    public float chaseSpeed = 4f;   // Chase movement speed when player is in range
    public float detectionRange = 10f;  // Detection range for the player
    public float impulseForce = 500f;  // Impulse force applied to the player
    public float explosionRadius = 5f;  // Radius of the explosion to apply force to nearby objects

    public ParticleSystem explosionEffect;  // Explosion particle effect (assign in inspector)

    private Transform player;   // Reference to the player
    private bool isChasing = false;  // Flag to check if the enemy is chasing the player
    private bool isExploded = false; // Flag to check if the explosion has occurred
    private Transform currentPatrolPoint;  // Current patrol point

    private Rigidbody rb;  // Rigidbody to apply movement and forces
    public PlayerHealthWithUI PlayerHealth;

    void Start()
    {
        // Set the initial patrol point to patrolPoint1
        currentPatrolPoint = patrolPoint1;
        rb = GetComponent<Rigidbody>();

        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Start patrolling
        StartCoroutine(Patrol());
    }

    void Update()
    {
        if (isExploded)
        {
            // Wait for the enemy to be destroyed after explosion
            return;
        }

        // Check if the player is within detection range
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            isChasing = true;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        // Move towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * chaseSpeed;

        // Optionally, rotate the enemy to face the player
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private IEnumerator Patrol()
    {
        while (!isChasing && !isExploded)
        {
            // Move between patrol points
            Vector3 direction = (currentPatrolPoint.position - transform.position).normalized;
            rb.velocity = direction * patrolSpeed;

            // Optionally, rotate the enemy to face the patrol point
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);

            // Check if we have reached the patrol point
            if (Vector3.Distance(transform.position, currentPatrolPoint.position) < 0.5f)
            {
                // Switch patrol points
                currentPatrolPoint = currentPatrolPoint == patrolPoint1 ? patrolPoint2 : patrolPoint1;
            }

            yield return null;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // If the enemy collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerExplosion();
            PlayerHealth.health -= 20f;
        }
    }

    private void TriggerExplosion()
    {
        if (isExploded) return;  // Avoid triggering the explosion multiple times

        isExploded = true;

        // Play the explosion particle effect (if assigned)
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Debug.Log("Explosion particle effect played!");
        }
        else
        {
            Debug.LogWarning("Explosion particle effect is missing!");
        }

        // Destroy the enemy after playing the explosion effects
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        
        // Apply the explosion force to objects tagged with "BlowBack" when the enemy is destroyed
        Collider[] blowBackObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in blowBackObjects)
        {
            // If the object has the "BlowBack" tag, apply the force to it
            if (col.CompareTag("blowBack"))
            {
                Rigidbody objectRb = col.GetComponent<Rigidbody>();
                if (objectRb != null)
                {
                    // Apply a force to the object
                    Vector3 forceDirection = (col.transform.position - transform.position).normalized;
                    objectRb.AddForce(forceDirection * impulseForce, ForceMode.Impulse);
                }
            }
        }
    }
}
