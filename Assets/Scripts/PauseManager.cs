using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    public GameObject pauseDialogue;

    private bool isPaused = false;

    private void Start()
    {
        Cursor.visible = false;
        pauseDialogue.SetActive(false);
    }

    // ESC pauses game
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            PauseGame(!isPaused);
        }
    }

    // Back to Title button load
    public void BackToTitle()
    {
        SceneManager.LoadScene(0);
    }
    // Set Time to 0 if game paused 
    public void PauseGame(bool doPause)
    {
        isPaused = doPause;
        pauseDialogue.SetActive(isPaused);

        Time.timeScale = isPaused ? 0 : 1;
    }

    // return Time to 1 if Game running
    public bool isGameRunning()
    {
        return (!isPaused);
    }

    // Destroy Menu if awake
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    // Quit Game
    public void QuitGame()
    {
        Application.Quit();
    }
}