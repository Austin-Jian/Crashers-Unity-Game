using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource audioSource; // Reference to the Audio Source component
    public GameObject gameOverUI;    // Reference to the Game Over UI

    void Start()
    {
        // Get the Audio Source component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check if the Game Over UI is active
        if (gameOverUI.activeInHierarchy)
        {
            StopMusic(); // Stop the music if the Game Over UI is shown
        }
    }

    public void StopMusic()
    {
        // Stop playing the background music
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
