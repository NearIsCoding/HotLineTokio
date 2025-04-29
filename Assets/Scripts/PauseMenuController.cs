using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("Configuración")]
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
        // Reanudar el tiempo
        Time.timeScale = 1f;

        // Cargar la escena del menú principal
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadConfig()
    {
        // Reanudar el tiempo
        Time.timeScale = 1f;

        // Cargar la escena del menú principal
        SceneManager.LoadScene("Config");
    }

    public void QuitGame()
    {
        
          Application.Quit();
       
    }

}