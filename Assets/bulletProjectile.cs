using UnityEngine;

public class bulletProjectile : MonoBehaviour
{
    Rigidbody bulletRigidbody;
    float speed = 10;
    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        bulletRigidbody.linearVelocity = transform.forward * speed;   
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
