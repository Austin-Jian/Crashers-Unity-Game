using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject roadSection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("proceduralGenerationTrigger"))
        {
            // Get the z position relative to the character and add 188 to it
            float newZPosition = other.transform.position.z + 184;
            // Instantiate the road section at the new position
            Instantiate(roadSection, new Vector3(180, 1, newZPosition), Quaternion.identity);
        }
    }
}
