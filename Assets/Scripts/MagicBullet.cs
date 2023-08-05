using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public float speed = 12f;
    public float lifetime = 1f;
      
    void Start()
    {
        // Spawns and destroys after lifetime
        Destroy(gameObject, lifetime);
    }

}