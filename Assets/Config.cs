using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfButtonManager : MonoBehaviour
{
    public void Back()
    {
        // Descargar la escena de Config
        SceneManager.UnloadSceneAsync("Config");
        Time.timeScale = 1.0f;
    }
}