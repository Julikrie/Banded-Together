using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed;
    public bool isFacingRight = true; // Start with facing right

    private int waypointIndex = 0;
    private Rigidbody2D rb;

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

        // Move towards the target waypoint
        rb.MovePosition(Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime));

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
    }
}