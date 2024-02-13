using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLvl : MonoBehaviour
{
    // Public variable witj the next scene name
    public string nextScene;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        }
    }
}
