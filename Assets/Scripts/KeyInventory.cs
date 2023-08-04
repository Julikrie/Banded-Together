using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
    
    public static KeyInventory Instance { get; private set; }

    public bool HasKey;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void PickUpKey()
    {
        HasKey = true;
    }

    public void UseKey()
    {
        HasKey = false;
    }
}
