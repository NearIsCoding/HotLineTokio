using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private string pauseMenuSceneName = "PauseMenu";    // Nombre de la escena del menú de pausa
    [SerializeField] private KeyCode pauseKey = KeyCode.P;               // Tecla para pausar

    private string gameSceneName;    // Guardará el nombre de la escena del juego
    private bool isPaused = false;

    void Start()
    {
        // Guardar el nombre de la escena actual (escena del juego)
        gameSceneName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        // Detectar cuando se presiona la tecla de pausa
        if (Input.GetKeyDown(pauseKey))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        // Pausar el tiempo
        Time.timeScale = 0f;

        // Cargar la escena de pausa de manera aditiva
        SceneManager.LoadSceneAsync(pauseMenuSceneName, LoadSceneMode.Additive);

        // Mostrar el cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        isPaused = true;
    }

    public void ResumeGame()
    {
        // Descargar la escena de pausa
        SceneManager.UnloadSceneAsync(pauseMenuSceneName);

        // Reanudar el tiempo
        Time.timeScale = 1f;

        isPaused = false;
    }
}