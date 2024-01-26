using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    // coloque aqui os dados da cena/level 
    public string currentScene;

    public GameData(GameObject player)
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

}
