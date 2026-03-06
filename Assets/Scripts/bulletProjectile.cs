using UnityEngine;

public class bulletProjectile : MonoBehaviour
{
    Rigidbody bulletRigidbody;
    float speed = 40;
    int damageAmount = 10;
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
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            //this hit an enemy
            enemy.EnemyDamaged(damageAmount);
        }
        Destroy(gameObject);
    }
}
