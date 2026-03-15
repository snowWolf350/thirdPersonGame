using StarterAssets;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("On Screen Buttons")]
    [SerializeField] Button _playButtonSS;
    [SerializeField] Button _creditsButtonSS;
    [SerializeField] Button _quitButtonSS;

    //SS is screen space WS is world Space
    [Header("Diegetic Buttons")]
    [SerializeField] Button _playButtonWS;

    [Header("Camera position")]
    [SerializeField] Transform _playCameraPosition;
    [SerializeField] Transform _startCameraPosition;

    Camera _mainCamera;

    MainMenu _mainMenuAction;
    private void Awake()
    {
        _mainMenuAction = new MainMenu();
        _mainCamera = Camera.main;

        _playButtonSS.onClick.AddListener(() => {
            StartCoroutine(MoveCamera(_playCameraPosition.position,_playCameraPosition.rotation));
        });
        _playButtonWS.onClick.AddListener(() => {
            SceneManager.LoadScene(1);
        });
    }
    private void OnEnable()
    {
        _mainMenuAction.menu.Escape.Enable();
    }
    private void OnDisable()
    {
        _mainMenuAction.menu.Escape.Disable();
        _mainMenuAction.menu.Escape.performed -= Escape_performed;
    }
    private void Start()
    {
        _mainMenuAction.menu.Escape.performed += Escape_performed;
    }

    private void Escape_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
       StartCoroutine(MoveCamera(_startCameraPosition.transform.position, _startCameraPosition.transform.rotation));
    }

    IEnumerator MoveCamera(Vector3 finalPosition,Quaternion finalRotation)
    {
        Vector3 startPosition = _mainCamera.transform.position;
        Quaternion startRotation = _mainCamera.transform.rotation;
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            _mainCamera.transform.position = Vector3.Lerp(startPosition, finalPosition, timer);
            _mainCamera.transform.rotation = Quaternion.Lerp(startRotation, finalRotation, timer);
            yield return null;
        }

        _mainCamera.transform.position = finalPosition;
        _mainCamera.transform.rotation = finalRotation;
    }
}
