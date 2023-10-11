using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    private bool isPaused = false;
    public GameObject pauseMenuCanvas;
    public string selectionSceneName = "Selection";
void Update()
{
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        TogglePause();
    }
}
public void TogglePause()
{
    isPaused = !isPaused;

    if (isPaused)
    {
        // Pause the game
        Time.timeScale = 0; // This freezes gameplay
        // Show the pause menu
        pauseMenuCanvas.SetActive(true);
    }
    else
    {
        // Unpause the game
        Time.timeScale = 1; // This resumes normal time scale
        // Hide the pause menu
        pauseMenuCanvas.SetActive(false);
    }
}
public void RestartGame()
{
    // Reset game state or load a new scene
    // (You can replace this with your actual game restart logic)
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

public void QuitGame()
{
   SceneManager.LoadScene(selectionSceneName);
}
}