using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed;
    public bool isFacingRight = true; // Start with facing right
    public Transform playerTransform;
    public float attackRange = 1.5f; // Adjust this to set the range at which the enemy starts attacking

    private int waypointIndex = 0;
    private Rigidbody2D rb;
    private bool isAttacking;
    private bool isMovingToPlayer; // Define this variable

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (waypoints.Length == 0)
        {
            return; // Ensure there are waypoints before proceeding
        }

        Vector2 targetPosition = waypoints[waypointIndex].position;
        Vector2 currentPosition = rb.position;

        // Move towards the target waypoint if not attacking
        if (!isAttacking && !isMovingToPlayer)
        {
            rb.MovePosition(Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime));
        }

        // Check if we are close enough to the waypoint
        if (Vector2.Distance(currentPosition, targetPosition) < 0.1f)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length; // Wrap around using modulo

            // Flip the facing direction when changing waypoints
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        // Check if close enough to start attacking
        if (isAttacking && playerTransform != null)
        {
            // Attack logic here (e.g., reduce player's health)
            // Implement your attack behavior based on your game's mechanics
        }
        else if (!isAttacking && isMovingToPlayer && playerTransform != null)
        {
            Vector3 moveDirection = (playerTransform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;

            // Check if close enough to start attacking
            if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
            {
                isAttacking = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = collision.gameObject.transform;
            isMovingToPlayer = true;
        }
    }
}