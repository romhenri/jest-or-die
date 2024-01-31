using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBehavior : MonoBehaviour
{
    public string scene;
    public string scene2;
    public string scene3;

    public void Options()
    {
        SceneManager.LoadScene(scene2);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(scene);
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
