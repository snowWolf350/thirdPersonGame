using UnityEngine;

public class KillZone : MonoBehaviour
{
    BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.TryGetComponent(out ThirdPersonShooterController playerController))
            {
                Debug.Log("running respawn");
                playerController.RespawnPlayer();
            }
        }
    }

}
