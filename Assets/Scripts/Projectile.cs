using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 5f;



    private Vector3 direction;

    void Start()
    {

        Destroy(gameObject, lifetime);
    }

    void Update()
    {

        transform.position += direction * speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 newDirection)
    {

        direction = newDirection.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

    }

}