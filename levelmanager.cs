using UnityEngine;
using UnityEngine.SceneManagement;

public class levelmanager : MonoBehaviour
{
    private void Update()
    {
        // Restart the scene if the "R" key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }

        // Exit the game if the "Esc" key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    private void RestartScene()
    {
        // Reload the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ExitGame()
    {
        // Exit the game
        Application.Quit();
    }
}
