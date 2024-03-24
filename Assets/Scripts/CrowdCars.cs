using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdCars : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of the car
    public float avoidRadius = 2f; // Radius to avoid other cars
    public LayerMask obstacleMask; // Layer mask for obstacles (other cars)

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = Random.insideUnitCircle.normalized; // Random initial movement direction
    }

    void FixedUpdate()
    {
        Move();
        AvoidObstacles();
    }

    void Move()
    {
        Vector2 currentPosition = transform.position;
        Vector2 newPosition = currentPosition + moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    void AvoidObstacles()
    {
        Collider2D[] obstacles = Physics2D.OverlapCircleAll(transform.position, avoidRadius, obstacleMask);

        if (obstacles.Length > 0)
        {
            Vector2 avoidDirection = Vector2.zero;
            foreach (Collider2D obstacle in obstacles)
            {
                avoidDirection += (Vector2)(transform.position - obstacle.transform.position).normalized;
            }

            moveDirection = Quaternion.Euler(0, 0, Random.Range(-30f, 30f)) * moveDirection; // Randomly adjust direction
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, avoidRadius);
    }
}
