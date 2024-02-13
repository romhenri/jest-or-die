using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject PauseScreen;

    void Start()
    {
    }

    void Update()
    {
        // Toggle pause screen
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (PauseScreen.activeSelf)
            {
                if (WinScreen.activeSelf)
                {
                    return;
                }

                UnsetPauseScreen();
            }
            else
            {
                if (WinScreen.activeSelf)
                {
                    return;
                }

                SetPauseScreen();
            }
        }

        // Continue after win screen (Space or Enter)
        if (WinScreen.activeSelf && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void SetWinScreen()
    {
        WinScreen.SetActive(true);
        
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
    }

    public void SetPauseScreen()
    {
        PauseScreen.SetActive(true);
        Time.timeScale = 0;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Pause();
    }

    public void UnsetPauseScreen()
    {
        PauseScreen.SetActive(false);
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
    }
}
