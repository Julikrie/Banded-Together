using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    public GameObject[] hearts;
    public int maxHearts = 3;
    public float invincibleDuration = 1f;
    private float currentInvincibleDuration = 0f;
    private int currentHearts;
    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        currentHearts = maxHearts;
        UpdateHeartDisplay();
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if(IsInvincible())
        {
            currentInvincibleDuration -= Time.deltaTime;
        }
    }


    public void DamageTaken(int damage)
    {
        Debug.Log("DamageTaken method called. Damage: " + damage + ", Current Hearts: " + currentHearts);
       
        if (!IsInvincible())
        {
            TakeDamage(damage);
        }
        

    }

    private void TakeDamage(int damage)
    {
        currentInvincibleDuration = invincibleDuration;
        currentHearts -= damage;
                
        UpdateHeartDisplay();
    }


    private bool IsInvincible()
    {
        return currentInvincibleDuration >= 0f;
    }

    private void UpdateHeartDisplay()
    {
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
