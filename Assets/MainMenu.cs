using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Configuraciones()
    {
        // Descargar la escena de MainMenu
        SceneManager.UnloadSceneAsync("MainMenu");
        // Cargar la escena de pausa de manera aditiva
        SceneManager.LoadSceneAsync("Config", LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}