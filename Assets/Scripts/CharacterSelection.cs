using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    private int currentCar;

    private void Awake()
    {
        SelectCar(0);
    }

    private void SelectCar(int _index)
    {
        previousButton.interactable = (_index != 0);
        nextButton.interactable = (_index != transform.childCount - 1);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }

        // Save the selected car's mesh and material
        MeshFilter meshFilter = transform.GetChild(_index).GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = transform.GetChild(_index).GetComponent<MeshRenderer>();

        if (meshFilter != null && meshRenderer != null)
        {
            SaveSelectedCharacter(meshFilter.mesh, meshRenderer.material);
        }
    }

    public void ChangeCar(int _change)
    {
        currentCar += _change;
        SelectCar(currentCar);
    }

    private void SaveSelectedCharacter(Mesh selectedMesh, Material selectedMaterial)
    {
        // Save the Mesh and Material to PlayerPrefs or another persistent storage method
        PlayerPrefs.SetString("SelectedMeshName", selectedMesh.name);
        PlayerPrefs.SetString("SelectedMaterialName", selectedMaterial.name);
    }
}
