using Unity.VisualScripting;
using UnityEngine;

public class bulletProjectile : MonoBehaviour
{
    Rigidbody _bulletRigidbody;
    [SerializeField]GameObject _BulletImpactFX;
    float speed = 40;
    int damageAmount = 10;
    private void Awake()
    {
        _bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _bulletRigidbody.linearVelocity = transform.forward * speed;   
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            //this hit an enemy
            enemy.EnemyDamaged(damageAmount);
        }
        Instantiate(_BulletImpactFX, collision.GetContact(0).point,Quaternion.identity);
        Destroy(gameObject);
    }
}
