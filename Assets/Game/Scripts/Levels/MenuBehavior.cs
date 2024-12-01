using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBehavior : MonoBehaviour
{
    [SerializeField]
    public string firstLevel;
    public string menu;
    public string credits;
    public string options;

    public string level1;
    public string level2;
    public string level3;
    public string level4;
    public string level5;

    [Header("UI")]
    [SerializeField] public GameObject mainMenu;
    [SerializeField] public GameObject levelSelection;
    [SerializeField] public GameObject aboutGame;

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(menu);
    }

    public void Options()
    {
        SceneManager.LoadScene(options);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void UI_MainMenu()
    {
        mainMenu.SetActive(true);
        aboutGame.SetActive(false);
        levelSelection.SetActive(false);
    }

    public void UI_LevelSelection() 
    {
        mainMenu.SetActive(false);
        aboutGame.SetActive(false);
        levelSelection.SetActive(true);
    }

    public void UI_AboutGame()
    {
        mainMenu.SetActive(false);
        aboutGame.SetActive(true);
        levelSelection.SetActive(false);
    }

    public void Level1()
    {
        SceneManager.LoadScene(level1);
    }

    public void Level2()
    {
        SceneManager.LoadScene(level2);
    }

    public void Level3()
    {
        SceneManager.LoadScene(level3);
    }

    public void Level4()
    {
        SceneManager.LoadScene(level4);
    }

    public void Level5()
    {
        SceneManager.LoadScene(level5);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
