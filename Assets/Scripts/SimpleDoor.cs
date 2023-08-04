using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : MonoBehaviour
{

    private void OpenDoor() 
    {
        KeyInventory.Instance.UseKey();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && KeyInventory.Instance.HasKey)
        {
            OpenDoor();
        }
    }
}
