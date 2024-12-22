using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PathEnemy : MonoBehaviour
{

    public Transform player;
    public Transform firePoint;
    public GameObject projectilePrefab;
    public Transform patrolPoint1;
    public Transform patrolPoint2;
    public float waitTimeAtPatrolPoint = 3f;
    public float maxShootingDistance = 30f;
    public float visionAngle = 45f;
    public LayerMask obstacleMask;
    public float fireRate = 0.5f;
    public float baseOffset = 0.5f;
    public float rotationSpeed = 5f;


    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Transform currentPatrolPoint;
    private bool isWaiting = false;
    private bool isShooting = false;
    private float nextFireTime = 0f;

    void Start()
    {

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();


        currentPatrolPoint = patrolPoint1;



        // Start patrolling
        MoveToPatrolPoint();
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            StopPatrolling();
            RotateTowardsPlayer();
            StartShooting();
        }
        else
        {
            StopShooting();
            Patrol();
        }
    }

    void Patrol()
    {
        if (isWaiting || navMeshAgent.pathPending) return;

        if (navMeshAgent.remainingDistance < 0.1f && !isWaiting)
        {
            StartCoroutine(WaitAtPatrolPoint());
        }
    }

    IEnumerator WaitAtPatrolPoint()
    {
        isWaiting = true;
        SetAnimatorState(isIdle: true, isRunning: false, isShooting: false);
        yield return new WaitForSeconds(waitTimeAtPatrolPoint);

        // Switch patrol points
        currentPatrolPoint = currentPatrolPoint == patrolPoint1 ? patrolPoint2 : patrolPoint1;
        MoveToPatrolPoint();
        isWaiting = false;
    }

    void MoveToPatrolPoint()
    {
        SetAnimatorState(isIdle: false, isRunning: true, isShooting: false);
        navMeshAgent.SetDestination(currentPatrolPoint.position);
    }

    void StopPatrolling()
    {
        navMeshAgent.ResetPath();
        SetAnimatorState(isIdle: true, isRunning: false, isShooting: false);
    }

    void RotateTowardsPlayer()
    {
        // Smoothly rotate the enemy to face the player
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void StartShooting()
    {
        if (!isShooting)
        {
            isShooting = true;
            SetAnimatorState(isIdle: false, isRunning: false, isShooting: true);
        }

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void StopShooting()
    {
        if (isShooting)
        {
            isShooting = false;
            SetAnimatorState(isIdle: false, isRunning: true, isShooting: false);
            MoveToPatrolPoint();
        }
    }

    void Shoot()
    {
        Vector3 predictedPosition = PredictPlayerPosition();
        Vector3 direction = (predictedPosition - firePoint.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.SetDirection(direction);
    }

    Vector3 PredictPlayerPosition()
    {
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        Vector3 playerVelocity = playerRb != null ? playerRb.velocity : Vector3.zero;
        float distance = Vector3.Distance(firePoint.position, player.position);
        float timeToHit = distance / projectilePrefab.GetComponent<Projectile>().speed;
        Vector3 predictedPosition = player.position + playerVelocity;

        predictedPosition.y += .5f;
        return predictedPosition;
    }

    bool CanSeePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > maxShootingDistance) return false;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer > visionAngle / 2) return false;

        if (Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask)) return false;

        return true;
    }

    void SetAnimatorState(bool isIdle, bool isRunning, bool isShooting)
    {
        animator.SetBool("IsIdle", isIdle);
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsShooting", isShooting);
    }

}