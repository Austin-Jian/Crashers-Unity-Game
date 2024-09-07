using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Load Main Game Scene (next in the list, e.g., after Main Menu)
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Go to the Character Selection Scene
    public void GoToCharacterSelection()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);  // Ensure the scene name matches exactly in the Build Settings
    }

    // Quit Game
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }
}
