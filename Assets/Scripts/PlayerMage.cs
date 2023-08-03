using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMage : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public GameObject playerKnight;
    public GameObject playerRogue;
    public GameObject magicBulletPrefab;
    public float magicBulletSpeed;
    public Transform magicBulletSpawn;
    public bool isFacingRight;
    public bool isWalking;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

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

        if (Input.GetKeyDown(KeyCode.F))
        {
            ShootMagic(isFacingRight);
        }

        ChangeOrientation();
        SwitchPlayer();
    }

    

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.8f, groundLayer);
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Pressing Q");
            rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer("Player");
            playerKnight.layer = LayerMask.NameToLayer("Default");
            Camera.main.GetComponent<CameraController>().SetActivePlayer(playerKnight.transform);

            playerKnight.GetComponent<PlayerKnight>().enabled = true;
            GetComponent<PlayerMage>().enabled = false;

            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            playerKnight.GetComponent<Rigidbody2D>().isKinematic = false;
        }
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
}

