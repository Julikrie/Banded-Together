using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public float speed = 12f;
    public float lifetime = 1.5f;
      
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}