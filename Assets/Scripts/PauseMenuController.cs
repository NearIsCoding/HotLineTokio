using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("Configuraci n")]
    [SerializeField] private string pauseMenuSceneName = "PauseMenu";
    private bool isPaused = false;
    [SerializeField] private PauseManager pauseManager;

    public void ResumeGame()
    {
        pauseManager.ResumeGame();
    }

    public void RestartLevel()
    {
        // Reanudar el tiempo
        Time.timeScale = 1f;

        // Obtener el nombre de la escena actual del juego
        //string gameScene = SceneManager.GetActiveScene().name;

        // Descargar la escena de pausa y recargar la escena del juego
        SceneManager.UnloadSceneAsync("PauseMenu");
        //SceneManager.LoadScene(gameScene);
    }

    public void LoadMainMenu()
    {
        // Descargar la escena de pausa
        SceneManager.UnloadSceneAsync(pauseMenuSceneName);
        // Cargar la escena de pausa de manera aditiva
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
    }

    public void LoadConfig()
    {
        // Descargar la escena de pausa
        SceneManager.UnloadSceneAsync(pauseMenuSceneName);
        // Cargar la escena de pausa de manera aditiva
        SceneManager.LoadSceneAsync("Config", LoadSceneMode.Additive);
    }

    public void QuitGame()
    {

        Application.Quit();

    }

}