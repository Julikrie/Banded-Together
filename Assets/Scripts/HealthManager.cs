using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    public GameObject[] hearts;
    public int maxHearts = 3;
    public float invincibleDuration = 1f;
    public AudioClip damageSound;
    public MMCameraShaker cameraShaker; // Feels Camerashake

    private AudioSource audioSource;
    private float currentInvincibleDuration = 0f;
    private int currentHearts;
    private void Awake()
    {
        // Make sure only one health manager exists
        if (Instance != null)
        {
            return;
        }

        currentHearts = maxHearts;
        UpdateHeartDisplay();
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Reduces time of Invincibility
        if(IsInvincible())
        {
            currentInvincibleDuration -= Time.deltaTime;
        }
      }


    public void DamageTaken(int damage)
    {
        // Player takes damage if not invincible
       
        if (!IsInvincible())
        {
            cameraShaker.ShakeCamera(1.2f, 1, 4, 1, 1, 1, false);
            TakeDamage(damage);
        }
        

    }

    private void TakeDamage(int damage)
    {
        //  Set Player invincible after taking damage and deals Damage
        currentInvincibleDuration = invincibleDuration;
        currentHearts -= damage;
                
        UpdateHeartDisplay();
        audioSource.PlayOneShot(damageSound, 1f);
    }


    private bool IsInvincible()
    {
        return currentInvincibleDuration >= 0f;
    }

    private void UpdateHeartDisplay()
    {
        // Updates the Heart UI
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentHearts); // Activate heart GameObject if i < currentHearts
        }
    }

    public bool IsPlayerDead()
    {
        return currentHearts <= 0;
    }
}
