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
        // Assuming the main menu scene is named "MainMenu"
        SceneManager.LoadScene("MainMenu");
    }
}
