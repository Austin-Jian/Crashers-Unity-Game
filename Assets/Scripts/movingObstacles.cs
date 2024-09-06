using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : MonoBehaviour
{
    public float moveSpeed = 0.25f;
    public float minX = 170f; // Public field to set in Unity Inspector
    public float maxX = 190f; // Public field to set in Unity Inspector
    private Rigidbody rb; // Reference to the Rigidbody

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Freeze rotation on the Rigidbody
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        Debug.Log("My name is " + gameObject.name + " and my position is " + transform.localPosition.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curPos = transform.localPosition;

        // Update the x position
        float newX = curPos.x + moveSpeed;

        // Reset position if it exceeds the defined boundaries
        if (newX > maxX)
        {
            newX = minX;
        }

        if (newX < minX)
        {
            newX = maxX;
        }

        // Apply the new position to the obstacle
        Vector3 newPos = new Vector3(newX, curPos.y, curPos.z);
        transform.localPosition = newPos;
    }
}
