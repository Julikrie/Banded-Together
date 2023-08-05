using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    public bool isDying;
    public AudioClip jumpSound;

    private AudioSource audioSource;
    private bool doubleJump;
    private Rigidbody2D rb;
    private Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheckRogue");
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Sets horizontal movement and animations
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        isWalking = Mathf.Abs(horizontalInput) > 0.01f;
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

        // Double Jump if player not grounded 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded() || doubleJump)
            {
                
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                doubleJump = !doubleJump;
                audioSource.PlayOneShot(jumpSound, 0.1f);
            }
        }

        ChangeOrientation();
        SwitchPlayer();
        Die();
    }
    private bool isGrounded()
    {
        // Ground check for Player
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
    public void ChangeOrientation()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput < 0f && isFacingRight || horizontalInput > 0f && !isFacingRight)
        {
            // Flip Character to moving direction
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void SwitchPlayer()
    {
        // Switch Player, stop ability to control the Characater and turn on Knight
        if (Input.GetKeyDown(KeyCode.E))
        {
            rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer("Player");
            playerKnight.layer = LayerMask.NameToLayer("Default");
            Camera.main.GetComponent<CameraController>().SetActivePlayer(playerKnight.transform);

            playerKnight.GetComponent<PlayerKnight>().enabled = true;
            GetComponent<PlayerRogue>().enabled = false;

            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            playerKnight.GetComponent<Rigidbody2D>().isKinematic = false;

        }

         // Switch Player, stop ability to control the Characater and turn on Mage
         if (Input.GetKeyDown(KeyCode.Q))
         {
            rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer("Player");
            playerMage.layer = LayerMask.NameToLayer("Default");
            Camera.main.GetComponent<CameraController>().SetActivePlayer(playerMage.transform);

            playerMage.GetComponent<PlayerMage>().enabled = true;
            GetComponent<PlayerRogue>().enabled = false;

            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            playerMage.GetComponent<Rigidbody2D>().isKinematic = false;
         }
    }
    public void Die()
    {
        if (HealthManager.Instance.IsPlayerDead())
        {
            // If player is dying reload scene after one second
            isDying = true;
            Invoke("ReloadScene", 1f);
        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}






