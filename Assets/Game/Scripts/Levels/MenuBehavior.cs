using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBehavior : MonoBehaviour
{
    public string cena;
    public string cena2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Options()
    {
        SceneManager.LoadScene(cena2); //Deletar ao iplementar opções
    }

    public void StartGame()
    {
        SceneManager.LoadScene(cena);
    }

    public void QuitGame()
    {
        //No Editor use a linha de código abaixo e comente a outra
        UnityEditor.EditorApplication.isPlaying = false;
        //quando o jogo for compilado use a linha de código abaixo e comente a outra
        //Application.Quit();
    }
}
