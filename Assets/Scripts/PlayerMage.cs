using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMage : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public GameObject playerKnightScript;
    public GameObject playerRogueScript;
    public GameObject magicBulletPrefab;
    public bool isFacingRight;
    public bool isWalking;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheckMage");
    }

    void Update()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        bool isWalking = Mathf.Abs(horizontalInput) > 0.01f;
        animator.SetBool("isWalking", isWalking);

        if (isWalking == true)
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        MagicBullet();
        ChangeOrientation();
        SwitchPlayer();
    }

    public void isJumping()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    public void ChangeOrientation()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput < 0f && isFacingRight || horizontalInput > 0f && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void MagicBullet()
    {
        if (Input.GetKeyDown(KeyCode.F) && isFacingRight)
        {
            Vector2 playerPosition = transform.position + new Vector3(+0.2f, 0);
            Instantiate(magicBulletPrefab, playerPosition, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.F) && !isFacingRight)
        {
            Vector2 playerPosition = transform.position + new Vector3(-0.2f, 0);
            Instantiate(magicBulletPrefab, playerPosition, Quaternion.identity);
        }
    }

    public void SwitchPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Pressing Q");
            rb.velocity = Vector2.zero;

            playerKnightScript.GetComponent<PlayerKnight>().enabled = true;
            GetComponent<PlayerMage>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            rb.velocity = Vector2.zero;

            playerRogueScript.GetComponent<PlayerRogue>().enabled = true;
            GetComponent<PlayerMage>().enabled = false;
        }
    }
}

