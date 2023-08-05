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
    public AudioClip jumpSound;
    
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private Animator animator;

    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheckKnight");
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Sets Horizontal movement and animations
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
            // Gives player possibility to grace period after leaving Floor to Jump
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0f)
        {
            // Jump
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            coyoteTimeCounter = 0f;
            audioSource.PlayOneShot(jumpSound, 0.1f);
        }
        ChangeOrientation();
        SwitchPlayer();
        Die();
    }
    private bool isGrounded()
    {
        // Grounded check
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
    public void ChangeOrientation()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput < 0f && isFacingRight || horizontalInput > 0f && !isFacingRight)
        {
            // Flip to walk direction
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void SwitchPlayer()
    {
        // Switch Player, stop ability to control the Characater and turn on Rogue
        if (Input.GetKeyDown(KeyCode.E))
        {
            rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer("Player");
            playerRogue.layer = LayerMask.NameToLayer("Default");
            Camera.main.GetComponent<CameraController>().SetActivePlayer(playerRogue.transform);

            playerRogue.GetComponent<PlayerRogue>().enabled = true;
            GetComponent<PlayerKnight>().enabled = false;

            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            playerRogue.GetComponent<Rigidbody2D>().isKinematic = false;
        }

        // Switch Player, stop ability to control the Characater and turn on Mage
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
            // If player is Dead reload Scene after 1 second
            isDying = true;
            Invoke("ReloadScene", 1f);

        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}
