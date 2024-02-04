using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject WinScreen;

    void Start()
    {
    }

    void Update()
    {
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
}
