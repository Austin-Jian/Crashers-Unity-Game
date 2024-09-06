using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float fixedYPosition = 10f; // The Y position to keep the camera at

    private Vector3 initialLocalPosition; // Store the initial local position of the camera relative to the player

    void Start()
    {
        // Store the initial local position of the camera
        initialLocalPosition = transform.localPosition;

        // Ensure the camera starts at the correct Y position
        transform.position = new Vector3(transform.position.x, fixedYPosition, transform.position.z);
    }

    void LateUpdate()
    {
        // Lock the camera's Y position to the specified fixed value
        transform.position = new Vector3(transform.position.x, fixedYPosition, transform.position.z);

        // Optionally, keep the initial local offset relative to the player in X and Z axes
        transform.localPosition = new Vector3(initialLocalPosition.x, transform.localPosition.y, initialLocalPosition.z);
    }
}
