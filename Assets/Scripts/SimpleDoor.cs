using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : MonoBehaviour
{
    private AudioSource audioSource;
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OpenDoor() 
    {
        // Uses the Key and removes the Gameobject
        KeyInventory.Instance.UseKey();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // On trigger play sound and remove door if the player has a Key
        if(collision.gameObject.CompareTag("Player") && KeyInventory.Instance.HasKey)
        {
            audioSource.Play();
            Invoke("OpenDoor", 1f);
        }
    }
}
