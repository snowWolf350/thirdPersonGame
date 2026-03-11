using UnityEngine;

public class Robot : MonoBehaviour
{
    Vector2 screenCentrePoint = new Vector2(Screen.width / 2, Screen.height / 2);
    Vector3 mouseWorldPosition = Vector3.zero;

    [SerializeField] LayerMask aimLayerMask;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(screenCentrePoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
            transform.rotation = Quaternion.LookRotation(mouseWorldPosition, Vector3.up);
        }
    }
}
