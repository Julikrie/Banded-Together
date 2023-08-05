using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform waypointA, waypointB;
    public int speed;
    Vector2 targetPos;
    void Start()
    {
        targetPos = waypointB.position;
    }

    void Update()
    { // Moving between Waypoints
        if (Vector2.Distance(transform.position, waypointA.position) < 0.1f) targetPos = waypointB.position;
        if (Vector2.Distance(transform.position, waypointB.position) < 0.1f) targetPos = waypointA.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Set Player to Child of MovingPlatform to not drop
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // On Exit of Platform switch back 
            if (collision.CompareTag("Player"))
            {
                collision.transform.SetParent(null);
            }
        }
    }

