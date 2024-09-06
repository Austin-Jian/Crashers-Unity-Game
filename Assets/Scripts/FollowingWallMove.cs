using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingWallMove : MonoBehaviour
{
    public float speed = 1f;  // Speed at which the wall moves
    public float pushForce = 5f;  // Force to apply to the player

    void Start()
    {
        // Manage layer collisions at the start of the game
        // Enable collisions between PlayerLayer and WallLayer
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerLayer"), LayerMask.NameToLayer("WallLayer"), false);

        // Disable collisions between WallLayer and other layers (replace with actual layers in your project)
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("WallLayer"), LayerMask.NameToLayer("Default"), true);
    }

    void Update()
    {
        // Move the wall forward (towards the player)
        transform.position -= new Vector3(0, 0, -1) * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object we collided with is tagged as "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            PushPlayer(collision.gameObject);
        }
    }

    private void PushPlayer(GameObject player)
    {
        // Get the Rigidbody of the player to apply a force
        Rigidbody playerRb = player.GetComponent<Rigidbody>();

        if (playerRb != null)
        {
            // Apply a force to push the player along the Z-axis (or whichever direction you're using)
            Vector3 pushDirection = new Vector3(0, 0, -1);  // Push along the Z-axis
            playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}
