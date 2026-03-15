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
    [SerializeField] Transform spawnTransform;

    StarterAssetsInputs starterAssetsInputs;
    ThirdPersonController thirdPersonController;

    Vector2 screenCentrePoint = new Vector2(Screen.width/2, Screen.height/2);
    Vector3 mouseWorldPosition = Vector3.zero;

    HealthSystem _healthSystem;

    public event EventHandler<IHasProgress.onProgressChangedEventArgs> onProgressChanged;
    public static event EventHandler OnPlayerShoot;
    public static event EventHandler OnPlayerDeath;

    private void Awake()
    {
        _healthSystem = new HealthSystem(100);
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }
    private void Start()
    {
        _healthSystem.onDeath += _healthSystem_onDeath;
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

        if (starterAssetsInputs.shoot && GameManager.Instance.GameIsPlaying())
        {
            OnPlayerShoot?.Invoke(this, EventArgs.Empty);
            Vector3 aimDir = (mouseWorldPosition - spawnBulletTransform.position).normalized;
            Instantiate(bulletPrefab,spawnBulletTransform.position,Quaternion.LookRotation(aimDir,Vector3.up));
            starterAssetsInputs.shoot = false;
        }
        if (starterAssetsInputs.escape)
        {
            if (GameManager.Instance.GameIsPaused() == false)
            {
                starterAssetsInputs.SetCursorLocked(false);
                GameManager.Instance.SetGameState(GameManager.gameState.paused);
            }
            else
            {
                starterAssetsInputs.SetCursorLocked(true);
                GameManager.Instance.SetGameState(GameManager.gameState.playing);
            }
            starterAssetsInputs.escape = false;
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

    private void _healthSystem_onDeath(object sender, EventArgs e)
    {
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        thirdPersonController.SetCanMove(false);
    }

    public void RespawnPlayer()
    {
        Vector3 playerOffset = new Vector3(0, 2, 0);

        CharacterController cc = GetComponent<CharacterController>();

        cc.enabled = false;

        transform.position = spawnTransform.position + playerOffset;

        cc.enabled = true;

        thirdPersonController.resetVerticalVelocity();

        _healthSystem.ResetHealth();
    }

}
