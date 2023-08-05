using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinalDoor : MonoBehaviour
{
    public Sprite startSprite;
    public Sprite activatedDoor;
    
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Collider2D triggerZone;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        triggerZone = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OpenDoor()
    {
        // Use Key change Door sprite to open
        KeyInventory.Instance.UseKey();
        spriteRenderer.sprite = activatedDoor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Compare if Player is entering with Key the Collider Zone
        if (collision.gameObject.CompareTag("Player") && KeyInventory.Instance.HasKey)
        {
            OpenDoor();
            Invoke("WinGame", 1f);
            audioSource.Play();
        }
    }
    private void WinGame()
    {
        SceneManager.LoadScene(0);
    }
}

