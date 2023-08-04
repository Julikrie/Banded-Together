using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameObject[] hearts;
    public int maxHearts = 3;
    private int currentHearts;

    private void Start()
    {
        currentHearts = maxHearts;
        UpdateHeartDisplay();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            DamageTaken(1); // You can adjust the damage value here
        }
    }

    public void DamageTaken(int damage)
    {
        Debug.Log("DamageTaken method called. Damage: " + damage + ", Current Hearts: " + currentHearts);

        currentHearts -= damage;

        if (currentHearts <= 0)
        {
            Debug.Log("Player is dead.");
            PlayerDead();
        }

        UpdateHeartDisplay();
    }

    private void UpdateHeartDisplay()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentHearts); // Activate heart GameObject if i < currentHearts
        }
    }

    private void PlayerDead()
    {
        // Add code here to handle player death, like game over screen or respawn logic
    }
}
