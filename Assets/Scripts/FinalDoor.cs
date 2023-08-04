using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinalDoor : MonoBehaviour
{
    public Sprite startSprite;
    public Sprite activatedDoor;
    private SpriteRenderer spriteRenderer;
    private Collider2D triggerZone;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        triggerZone = GetComponent<Collider2D>();
    }

    private void OpenDoor()
    {
        KeyInventory.Instance.UseKey();
        spriteRenderer.sprite = activatedDoor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && KeyInventory.Instance.HasKey)
        {
            OpenDoor();
            Invoke("WinGame", 1f);
        }
    }
    private void WinGame()
    {
        SceneManager.LoadScene(0);
    }
}

