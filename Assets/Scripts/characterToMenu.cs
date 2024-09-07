using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    // Load the Main Menu scene
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);  // This loads the Main Menu using the build index (0 as per your build settings)
    }
}
