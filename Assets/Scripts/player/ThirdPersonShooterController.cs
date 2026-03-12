using UnityEngine;
using Unity.Cinemachine;
using StarterAssets;
using System;

public class ThirdPersonShooterController : MonoBehaviour,IHasProgress,IDamagable
{
    [SerializeField] CinemachineCamera aimVirtualCamera;
    [SerializeField] float normalSensitivity;
    [SerializeField] float aimSensitivity;
    [SerializeField] LayerMask aimLayerMask = new LayerMask();
    [SerializeField] Transform bulletPrefab;
    [SerializeField] Transform debugPrefab;
    [SerializeField] Transform spawnBulletTransform;

    StarterAssetsInputs starterAssetsInputs;
    ThirdPersonController thirdPersonController;

    Vector2 screenCentrePoint = new Vector2(Screen.width/2, Screen.height/2);
    Vector3 mouseWorldPosition = Vector3.zero;

    HealthSystem _healthSystem;

    public event EventHandler<IHasProgress.onProgressChangedEventArgs> onProgressChanged;

    private void Awake()
    {
        _healthSystem = new HealthSystem(100);
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }
    private void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(screenCentrePoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
            //debugPrefab.position = raycastHit.point;
        }

        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.setRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.setRotateOnMove(true);
        }

        if (starterAssetsInputs.shoot)
        {
            Vector3 aimDir = (mouseWorldPosition - spawnBulletTransform.position).normalized;
            Instantiate(bulletPrefab,spawnBulletTransform.position,Quaternion.LookRotation(aimDir,Vector3.up));
            starterAssetsInputs.shoot = false;
        }
    }

    public void TakeDamage(int damageAmount, Vector3 damagePoint)
    {
        _healthSystem.TakeDamage(damageAmount);
        onProgressChanged?.Invoke(this, new IHasProgress.onProgressChangedEventArgs
        {
            progressNormalized = _healthSystem.GetHealthNormalized()
        });
    }
}
