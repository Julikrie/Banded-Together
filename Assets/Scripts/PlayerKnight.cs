using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerKnight : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public GameObject playerRogue;
    public GameObject playerMage;
    public bool isFacingRight;
    public bool isWalking;
    public bool isDying;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;

    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheckKnight");
    }

    void Update()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        bool isWalking = Mathf.Abs(horizontalInput) > 0.01f;
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isDying", isDying);

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

        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            coyoteTimeCounter = 0f;
        }
        ChangeOrientation();
        SwitchPlayer();
        Die();
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
            Debug.Log("Pressing E");
            rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer("Player");
            playerRogue.layer = LayerMask.NameToLayer("Default");
            Camera.main.GetComponent<CameraController>().SetActivePlayer(playerRogue.transform);

            playerRogue.GetComponent<PlayerRogue>().enabled = true;
            GetComponent<PlayerKnight>().enabled = false;

            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            playerRogue.GetComponent<Rigidbody2D>().isKinematic = false;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rb.velocity = Vector2.zero; 
            gameObject.layer = LayerMask.NameToLayer("Player");
            playerMage.layer = LayerMask.NameToLayer("Default");
            Camera.main.GetComponent<CameraController>().SetActivePlayer(playerMage.transform);

            playerMage.GetComponent<PlayerMage>().enabled = true;
            GetComponent<PlayerKnight>().enabled = false;

            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            playerMage.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
    public void Die()
    {if (HealthManager.Instance.IsPlayerDead())
        {
            isDying = true;
            Invoke("ReloadScene", 1f);
        }     
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}
