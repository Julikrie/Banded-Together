using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwo : MonoBehaviour
{
    public float speed;
    public GameObject playerOneScript;

    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Sets movement 
        rb.velocity = new Vector2(horizontalInput * speed, 0f);
        SwitchPlayer();
    }

    // Switches playable Characters
    public void SwitchPlayer()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressing E");
            rb.velocity = Vector2.zero;

            playerOneScript.GetComponent<PlayerController>().enabled = true;
            GetComponent<PlayerTwo>().enabled = false;
        }
    }
}

