using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMage : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public GameObject playerKnight;
    public GameObject playerRogue;
    public GameObject magicBulletPrefab;
    public float magicBulletSpeed;
    public Transform magicBulletSpawn;
    public AudioClip magicBulletSound;
    public AudioClip jumpSound;
    public bool isFacingRight;
    public bool isWalking;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isDying;

    private AudioSource audioSource;
    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    private Rigidbody2D rb;
    private Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheckMage");
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Set horizontal movement and animations
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

        // Grace period to jump when leaving Floor
        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Space to Jump 
        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            coyoteTimeCounter = 0f;
            audioSource.PlayOneShot(jumpSound, 0.1f);
        }

        // Shoot MagicBullet with "F"
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShootMagic(isFacingRight);
            audioSource.PlayOneShot(magicBulletSound, 0.2f);
        }

        ChangeOrientation();
        SwitchPlayer();
        Die();
    }

    

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
    public void ChangeOrientation()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput < 0f && isFacingRight || horizontalInput > 0f && !isFacingRight)
        {
            // Flip Character in walking Direction
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // Set MagicBullet in facing direction of Character
    void ShootMagic(bool isFacingRight)
    {
        GameObject bullet = Instantiate(magicBulletPrefab, magicBulletSpawn.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        float distanceToMove = 2f * Time.deltaTime;

        if (isFacingRight)
        {
            bulletRb.velocity = Vector2.right * magicBulletSpeed;
            bullet.transform.rotation = Quaternion.Euler(0, 0, 180); 
        }
        else
        {
            bulletRb.velocity = Vector2.left * magicBulletSpeed;
            bullet.transform.rotation = Quaternion.Euler(0, 0, 0); 
        }
    }

    public void SwitchPlayer()
    {
        // Switch Player, stop ability to control the Characater and turn on Knight

        if (Input.GetKeyDown(KeyCode.Q))
        {
            rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer("Player");
            playerKnight.layer = LayerMask.NameToLayer("Default");
            Camera.main.GetComponent<CameraController>().SetActivePlayer(playerKnight.transform);

            playerKnight.GetComponent<PlayerKnight>().enabled = true;
            GetComponent<PlayerMage>().enabled = false;

            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            playerKnight.GetComponent<Rigidbody2D>().isKinematic = false;
        }

        // Switch Player, stop ability to control the Characater and turn on Rogue
        if (Input.GetKeyDown(KeyCode.E))
        {
            rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer("Player");
            playerRogue.layer = LayerMask.NameToLayer("Default");
            Camera.main.GetComponent<CameraController>().SetActivePlayer(playerRogue.transform);

            playerRogue.GetComponent<PlayerRogue>().enabled = true;
            GetComponent<PlayerMage>().enabled = false;

            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            playerRogue.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
    public void Die()
    {
        if (HealthManager.Instance.IsPlayerDead())
        {
            // If is dying reload scene after one second
            isDying = true;
            Invoke("ReloadScene", 1f);

        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}
