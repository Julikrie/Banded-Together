using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBlocks : MonoBehaviour
{
    public Sprite activatedBlock;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // Get the SpriteRenderer component attached to this game object
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Magic Bullet"))
        {
            Destroy(collision.gameObject);
            gameObject.layer = LayerMask.NameToLayer("Floor");

            spriteRenderer.sprite = activatedBlock;
        }
    }
}
