using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
    private AudioSource audioSource;

    public static KeyInventory Instance { get; private set; }

    public bool HasKey;

    private void Awake()
    {
        // Make sure only one KeyInventory exists
        if (Instance != null)
        {
            return;
        }

        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PickUpKey()
    {
        // Looks if Player has key
        HasKey = true;
        audioSource.Play();
    }

    public void UseKey()
    {
        // Uses Key and removes Key
        HasKey = false;
    }
}
