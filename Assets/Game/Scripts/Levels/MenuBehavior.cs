using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBehavior : MonoBehaviour
{
    [SerializeField]
    public string firstLevel;
    public string credits;
    public string scene;
    public string scene2;
    public string scene3;

    public void StartGame()
    {
        SceneManager.LoadScene(scene);
    }

    public void Options()
    {
        SceneManager.LoadScene(scene2);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Cena3()
    {
        SceneManager.LoadScene(scene3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
