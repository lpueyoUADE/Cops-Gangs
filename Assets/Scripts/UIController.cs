using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Pause")]
    public GameObject pausePanel;
    public GameObject pauseButton;
    public GameObject miniMap;

    public TextMeshProUGUI moneyAmount;
    public TextMeshProUGUI ammoAmount;
    public RyderModel ryderReference;

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

    private void ToggleMiniMap()
    {
        if (miniMap)
        {
            miniMap.SetActive(!miniMap.activeSelf);
            moneyAmount.gameObject.transform.parent.gameObject.SetActive(!miniMap.activeSelf);
            ammoAmount.gameObject.transform.parent.gameObject.SetActive(!miniMap.activeSelf);
        }

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

        if(Input.GetKeyDown(KeyCode.Tab) && !isPaused)
        {
            ToggleMiniMap();
        }

        if (ryderReference != null)
        {
            moneyAmount.text = ryderReference.CurrentMoney.ToString();
            ammoAmount.text = ryderReference.CurrentAmmo.ToString() + " - " + ryderReference.MaxAmmo.ToString();
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
