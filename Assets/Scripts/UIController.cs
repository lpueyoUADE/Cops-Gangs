using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("Pause")]
    public GameObject pausePanel;
    public GameObject pauseButton;

    bool isPaused;

    private void Start()
    {
        UnPause();
    }
    public void Pause()
    {
        if (pausePanel) pausePanel.SetActive(true);
        if (pauseButton) pauseButton.SetActive(false);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        if (pausePanel) pausePanel.SetActive(false);
        if (pauseButton) pauseButton.SetActive(true);
        isPaused = false;
        Time.timeScale = 1;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                UnPause();
            else
                Pause();
        }
    }
    public void GoToNewGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
