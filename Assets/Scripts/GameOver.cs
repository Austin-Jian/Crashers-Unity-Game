using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // This method is called when the "RESTART" button is clicked
    public void RestartGame()
    {
        // Reload the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // This method is called when the "Main Menu" button is clicked
    public void LoadMainMenu()
    {
        // Load the scene named "mainMenu" as per the build settings
        SceneManager.LoadScene("mainMenu");
    }
}
