using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<bulletProjectile>() != null)
            {
                //this is a bullet
                other.GetComponent<bulletProjectile>().DestroySelf();
            }
            else
            {
                //kill the player
                Debug.Log("Player fell");
            }
        }
    }
}
