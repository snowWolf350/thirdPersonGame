using UnityEngine;
using Unity.Cinemachine;
using StarterAssets;
using TreeEditor;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] CinemachineCamera aimVirtualCamera;
    [SerializeField] float normalSensitivity;
    [SerializeField] float aimSensitivity;
    [SerializeField] LayerMask aimLayerMask = new LayerMask();
    [SerializeField] Transform debugTransform;

    StarterAssetsInputs starterAssetsInputs;
    ThirdPersonController thirdPersonController;

    Vector2 screenCentrePoint = new Vector2(Screen.width/2, Screen.height/2);


    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }
    private void Update()
    {
        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
        }

        Ray ray = Camera.main.ScreenPointToRay(screenCentrePoint);
        if (Physics.Raycast(ray,out RaycastHit raycastHit, 999f, aimLayerMask))
        {
            debugTransform.position = raycastHit.point;
        }
    }
}
