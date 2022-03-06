using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject gameCanvas;
    public GameObject pauseMenuCanvas;
    public GameObject gameOverCanvas;
    public GameManager gameManager;
    public GameObject playerPrefab;
    public Transform bulletsFolder;
    public Transform enemiesParent;
    public Transform playerSpawnPoint;
    private GameObject player;

    private void Start()
    {
        OpenMainMenuScreen();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameCanvas.activeInHierarchy) OpenPauseScreen();
            else if (pauseMenuCanvas.activeInHierarchy) ContinueToGame();
        }
    }

    private void DisableAllScreens()
    {
        mainMenuCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    public void OpenMainMenuScreen()
    {
        ClearGame();
        DisableAllScreens();
        mainMenuCanvas.SetActive(true);
    }

    public void OpenGameScreen()
    {
        if (Time.timeScale == 0) gameManager.StartOrStopTime();
        player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        gameManager.enabled = true;
        gameManager.RestartGame();
        DisableAllScreens();
        gameCanvas.SetActive(true);
    }

    public void ContinueToGame()
    {
        gameManager.enabled = true;
        gameManager.StartOrStopTime();
        DisableAllScreens();
        gameCanvas.SetActive(true);

    }

    public void RestartGame()
    {
        ClearGame();
        gameManager.enabled = true;
        if (Time.timeScale == 0) gameManager.StartOrStopTime();
        gameManager.RestartGame();
        player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        DisableAllScreens();
        gameCanvas.SetActive(true);
    }

    public void OpenPauseScreen()
    {
        DisableAllScreens();
        pauseMenuCanvas.SetActive(true);
        gameManager.StartOrStopTime();
        gameManager.enabled = false;
    }

    public void OpenGameOverScreen()
    {
        Destroy(player);
        gameManager.StartOrStopTime();
        gameManager.enabled = false;
        DisableAllScreens();
        gameOverCanvas.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void ClearGame()
    {
        gameManager.enabled = false;
        foreach (Transform bullet in bulletsFolder)
        {
            Destroy(bullet.gameObject);
        }
        foreach (Transform enemy in enemiesParent)
        {
            Destroy(enemy.gameObject);
        }
        if (player != null) Destroy(player);
    }
}
