using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject characterPrefab;  // The prefab object to apply the mesh and material

    private void Start()
    {
        LoadSelectedCharacter();
    }

    private void LoadSelectedCharacter()
    {
        string selectedMeshName = PlayerPrefs.GetString("SelectedMeshName");
        string selectedMaterialName = PlayerPrefs.GetString("SelectedMaterialName");

        if (!string.IsNullOrEmpty(selectedMeshName) && !string.IsNullOrEmpty(selectedMaterialName))
        {
            MeshFilter meshFilter = characterPrefab.GetComponent<MeshFilter>();
            MeshRenderer meshRenderer = characterPrefab.GetComponent<MeshRenderer>();

            if (meshFilter != null && meshRenderer != null)
            {
                // Load the mesh and material by name from resources or a preloaded library
                Mesh loadedMesh = FindMeshByName(selectedMeshName);
                Material loadedMaterial = FindMaterialByName(selectedMaterialName);

                if (loadedMesh != null && loadedMaterial != null)
                {
                    meshFilter.mesh = loadedMesh;
                    meshRenderer.material = loadedMaterial;
                }
            }
        }
    }

    private Mesh FindMeshByName(string meshName)
    {
        // Implement loading the mesh by name (it can be from resources or a preloaded list)
        return Resources.Load<Mesh>(meshName);
    }

    private Material FindMaterialByName(string materialName)
    {
        // Implement loading the material by name (it can be from resources or a preloaded list)
        return Resources.Load<Material>(materialName);
    }
}
