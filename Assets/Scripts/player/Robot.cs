using UnityEngine;

public class Robot : MonoBehaviour
{
    Vector2 screenCentrePoint = new Vector2(Screen.width / 2, Screen.height / 2);
    Vector3 mouseWorldPosition = Vector3.zero;
    Vector3 aimDirection;

    [SerializeField] LayerMask aimLayerMask;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(screenCentrePoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
            aimDirection = mouseWorldPosition - transform.position;
            transform.rotation = Quaternion.LookRotation(aimDirection, Vector3.up);
        }
    }
}
