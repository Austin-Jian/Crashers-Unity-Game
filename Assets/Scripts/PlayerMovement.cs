using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshPro namespace

public class PlayerMovement : MonoBehaviour
{
    public float moveOffset = 1f;
    public float jumpHeight = 0.2f;
    public float jumpDuration = 0.1f;
    public GameObject gameOverUI;
    public TMP_Text highestScoreText;  // Reference to a TMP_Text for displaying the highest score
    public TMP_Text scoreText;         // Reference to a TMP_Text for displaying the current score
    public SectionTrigger sectionTrigger;

    public AudioClip jumpClip;
    public AudioClip deathClip;

    private AudioSource audioSource;
    private Rigidbody rb;
    private bool isGameOver = false;
    private bool isJumping = false;

    private float minX = 173f;
    private float maxX = 187f;

    // Score variables
    private int score = 0;
    private int highestScore = 0;  // Track highest score

    void Start()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
        else
        {
            Debug.LogError("Game Over UI is not assigned in the Inspector!");
        }

        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            Debug.LogError("Rigidbody component is missing on the player object!");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on the player object!");
        }

        // Load the highest score from PlayerPrefs
        highestScore = PlayerPrefs.GetInt("HighestScore", 0);  // Default value is 0 if none is saved

        // Initialize the score display
        UpdateScoreUI();
    }

    void Update()
    {
        if (isGameOver || isJumping) return;

        float xOffset = 0;
        float zOffset = 0;
        bool hasMoved = false;

        // Input handling for movement along X and Z axes
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            xOffset = moveOffset;
            hasMoved = true;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            xOffset = -moveOffset;
            hasMoved = true;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            zOffset = -moveOffset;
            hasMoved = true;

            // Decrease score when W is pressed
            UpdateScore(-1);
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            zOffset = moveOffset;
            hasMoved = true;

            // Increase score when S is pressed
            UpdateScore(1);
        }

        // If movement is triggered, calculate the target position and clamp the X value
        if (hasMoved)
        {
            Vector3 targetPosition = new Vector3(
                Mathf.Clamp(transform.position.x + xOffset, minX, maxX), // Clamp X between minX and maxX
                transform.position.y,
                transform.position.z + zOffset
            );

            // Check if the player is blocked by any walls
            if (!IsBlockedByWall(targetPosition))
            {
                StartCoroutine(JumpToPosition(targetPosition));

                // Play jump sound
                if (audioSource != null && jumpClip != null)
                {
                    audioSource.PlayOneShot(jumpClip);
                }
            }
        }
    }

    private IEnumerator JumpToPosition(Vector3 targetPosition)
    {
        isJumping = true;

        Vector3 startPosition = transform.position;
        Vector3 peakPosition = new Vector3(startPosition.x, startPosition.y + jumpHeight, startPosition.z);

        float elapsedTime = 0f;
        while (elapsedTime < jumpDuration / 2)
        {
            float t = elapsedTime / (jumpDuration / 2);
            transform.position = Vector3.Lerp(startPosition, peakPosition, Mathf.SmoothStep(0, 1, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < jumpDuration / 2)
        {
            float t = elapsedTime / (jumpDuration / 2);
            transform.position = Vector3.Lerp(peakPosition, targetPosition, Mathf.SmoothStep(0, 1, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isJumping = false;
    }

    // Method to check if the movement is blocked by a wall using Raycast
    private bool IsBlockedByWall(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPosition);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, distance))
        {
            // If the player hits a "followWall", trigger game over
            if (hit.collider.CompareTag("followWall"))
            {
                TriggerGameOver();
                return true; // Block movement by ending the game
            }
            // If the player hits a "frontWall", block movement but don't trigger game over
            else if (hit.collider.CompareTag("frontWall"))
            {
                return true; // Block movement without game over
            }
        }

        return false; // No wall is blocking the movement
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Game over if colliding with MovingObstacle or followWall
        if (!isGameOver && (collision.gameObject.CompareTag("MovingObstacle") || collision.gameObject.CompareTag("followWall")))
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        isGameOver = true;
        rb.constraints = RigidbodyConstraints.FreezeAll; // Freeze all movement

        // Play death sound
        if (audioSource != null && deathClip != null)
        {
            audioSource.PlayOneShot(deathClip);
        }

        // Check if the current score is higher than the saved highest score
        if (score > highestScore)
        {
            highestScore = score;
            PlayerPrefs.SetInt("HighestScore", highestScore);  // Save the new highest score
        }

        // Display the Game Over UI and the highest score
        if (gameOverUI != null)
        {
            highestScoreText.text = "Highest Score: " + highestScore;
            gameOverUI.SetActive(true);
        }
        else
        {
            Debug.LogError("Game Over UI is not assigned in the Inspector!");
        }
    }

    // Update the score and UI
    private void UpdateScore(int value)
    {
        // Update the score, but ensure it doesn't drop below 0
        score = Mathf.Max(0, score + value);
        UpdateScoreUI();
    }

    // Update the score text in the UI using TMP_Text
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();  // Display only the score
        }
        else
        {
            Debug.LogError("Score Text (TMP) is not assigned in the Inspector!");
        }
    }
}
