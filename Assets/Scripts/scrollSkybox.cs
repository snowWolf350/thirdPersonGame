using UnityEngine;

public class scrollSkybox : MonoBehaviour
{
    [SerializeField] Material _skyBoxMat;
    [SerializeField] float _scrollSpeed;

    private void Start()
    {
        RenderSettings.skybox = _skyBoxMat;
    }

    private void Update()
    {
        float currentRotation = RenderSettings.skybox.GetFloat("_Rotation");

        // Calculate the new rotation value based on time and speed
        // The rotation is typically around the Y-axis
        float newRotation = currentRotation + Time.deltaTime * _scrollSpeed;

        // Set the new rotation value back to the skybox material
        RenderSettings.skybox.SetFloat("_Rotation", newRotation);
    }
}
