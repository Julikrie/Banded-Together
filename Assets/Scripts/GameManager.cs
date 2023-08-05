using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
        // sets Play Time to default
    }
    // Load GameScene
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    // Quit Game
    public void OuitGame() 
    {
        Application.Quit();
    }
}



