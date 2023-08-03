using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBlocks : MonoBehaviour
{
    public Sprite activatedBlock;
    public Sprite startSprite;
    private SpriteRenderer spriteRenderer;
    private Collider2D colliderHiddenObjects; 


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = startSprite;

        colliderHiddenObjects = GetComponent<Collider2D>();
        colliderHiddenObjects.isTrigger = true; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (collision.gameObject.CompareTag("Magic Bullet"))
            {
                gameObject.layer = LayerMask.NameToLayer("Floor");

                spriteRenderer.sprite = activatedBlock;
                Destroy(collision.gameObject);

                colliderHiddenObjects.isTrigger = false;
            }
        }
    }
}
   
