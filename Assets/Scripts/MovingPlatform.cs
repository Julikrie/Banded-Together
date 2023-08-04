using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform waypointA, waypointB;
    public int speed;
    Vector2 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        targetPos = waypointB.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, waypointA.position) < 0.1f) targetPos = waypointB.position;
        if (Vector2.Distance(transform.position, waypointB.position) < 0.1f) targetPos = waypointA.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            if (collision.CompareTag("Player"))
            {
                collision.transform.SetParent(null);
            }
        }
    }

