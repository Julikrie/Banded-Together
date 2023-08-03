using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRogue : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public GameObject playerKnight;
    public GameObject playerMage;
    public bool isFacingRight;
    public bool isWalking;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private bool doublejump;
    private Rigidbody2D rb;
    private Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheckRogue");
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
        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if(isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            doublejump = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0f || doublejump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            doublejump = !doublejump;
            coyoteTimeCounter = 0f;
        }

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

    public void SwitchPlayer()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer("Player");
            playerKnight.layer = LayerMask.NameToLayer("Default");

            playerKnight.GetComponent<PlayerKnight>().enabled = true;
            GetComponent<PlayerRogue>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer("Player");
            playerMage.layer = LayerMask.NameToLayer("Default");

            playerMage.GetComponent<PlayerMage>().enabled = true;
      
      GetComponent<PlayerRogue>().enabled = false;
        }
    }
}
