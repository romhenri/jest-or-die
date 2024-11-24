using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject PauseScreen;

    public string winScreenTargetScene;
    public string levelSelectionScene;

    public int level = -1;
    public int coinsAvailable = 4;

    void Start()
    {
        if (Timer.instance != null)
        {
            Timer.instance.StartTimer();
        }
        else
        {
            Debug.LogWarning("Timer instance is null. Make sure Timer is initialized.");
        }
    }

    void Update()
    {
        HandlePauseInput();

        // Reload (F5)
        if (Input.GetKeyDown(KeyCode.F5))
        {
            ReloadCurrentLevel();
        }

        // Win (F1 + F2)
        if (Input.GetKeyDown(KeyCode.F1) && Input.GetKeyDown(KeyCode.F2))
        {
            UnsetPauseScreen();
            SetWinScreen();
        }

        HandleWinScreenInput();

        HandleLevelSelectionInput();
    }

    void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (WinScreen != null && WinScreen.activeSelf)
            {
                UnsetPauseScreen();
                NavMainMenu();
            }
            else if (PauseScreen != null)
            {
                if (PauseScreen.activeSelf)
                {
                    UnsetPauseScreen();
                }
                else
                {
                    SetPauseScreen();
                }
            }
        }
    }

    void HandleWinScreenInput()
    {
        if (WinScreen != null && WinScreen.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                if (!string.IsNullOrEmpty(winScreenTargetScene))
                {
                    NextLevel();
                }
                else
                {
                    Debug.LogWarning("winScreenTargetScene não foi atribuído.");
                }
            }
        }
    }

    void HandleLevelSelectionInput()
    {
        if ((PauseScreen != null && PauseScreen.activeSelf) ||
            (WinScreen != null && WinScreen.activeSelf))
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                UnsetPauseScreen();
                SceneManager.LoadScene(levelSelectionScene);
            }
        }
    }

    void ReloadCurrentLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void SetWinScreen()
    {
        if (WinScreen != null)
        {
            WinScreen.SetActive(true);
        }
        else
        {
            Debug.LogWarning("WinScreen GameObject is not assigned.");
        }

        if (Timer.instance != null)
        {
            Timer.instance.StopTimer();
        }
        else
        {
            Debug.LogWarning("Timer instance is null.");
        }

        // Stop all spawners
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spawner in spawners)
        {
            spawner.SetActive(false);
        }

        // Destroy all knives
        GameObject[] knives = GameObject.FindGameObjectsWithTag("Knife");
        foreach (GameObject knife in knives)
        {
            Destroy(knife);
        }

        if (GameController.instance != null)
        {
            int _level = GameController.instance.GetLevel();
            if (level > _level)
            {
                GameController.instance.SetLevel(level);
            }
            GameController.instance.IncreaseCoins(coinsAvailable);
            //GameController.instance.SaveNow();
        }
        else
        {
            Debug.LogWarning("GameController instance is null. Make sure GameController is initialized.");
        }
    }

    public void SetPauseScreen()
    {
        if (PauseScreen != null)
        {
            PauseScreen.SetActive(true);
        }
        else
        {
            Debug.LogWarning("PauseScreen GameObject is not assigned.");
        }

        if (Timer.instance != null)
        {
            Timer.instance.StopTimer();
        }

        Time.timeScale = 0;

        AudioSource mainCameraAudio = GameObject.FindGameObjectWithTag("MainCamera")?.GetComponent<AudioSource>();
        if (mainCameraAudio != null)
        {
            mainCameraAudio.Pause();
        }
        else
        {
            Debug.LogWarning("MainCamera or AudioSource component is missing.");
        }
    }

    public void UnsetPauseScreen()
    {
        if (PauseScreen != null)
        {
            PauseScreen.SetActive(false);
        }

        if (Timer.instance != null)
        {
            Timer.instance.ResetTimer();
        }

        Time.timeScale = 1;

        AudioSource mainCameraAudio = GameObject.FindGameObjectWithTag("MainCamera")?.GetComponent<AudioSource>();
        if (mainCameraAudio != null)
        {
            mainCameraAudio.Play();
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(winScreenTargetScene);
    }

    public void NavMainMenu()
    {
        UnsetPauseScreen();
        SceneManager.LoadScene(levelSelectionScene);
    }
}
