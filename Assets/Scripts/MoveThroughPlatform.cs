using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThroughPlatform : MonoBehaviour
{
    private GameObject oneWayPlatform;
    public BoxCollider2D playerCollider;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if(oneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("OneWayPlatform"))
        {
            oneWayPlatform = collision.gameObject;
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            oneWayPlatform = null;
        }

    }
    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = oneWayPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}
