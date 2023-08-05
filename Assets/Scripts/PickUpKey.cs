using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // Pick up Key and add to Inventory
            KeyInventory.Instance.PickUpKey();
            Invoke("OnTriggerEnter2D", 1f);
            Destroy(gameObject);
        }
    }
}
