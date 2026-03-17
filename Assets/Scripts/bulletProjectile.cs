using System;
using UnityEngine;

public class bulletProjectile : MonoBehaviour
{
    Rigidbody _bulletRigidbody;
    [SerializeField]GameObject _BulletImpactFX;
    float speed = 40;
    int damageAmount = 10;

    public bool damagePlayer;

    public static event EventHandler<OnBulletImpactEventArgs> OnBulletImpact;

    public class OnBulletImpactEventArgs:EventArgs
    {
        public Vector3 impactPosition;
    }

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
        OnBulletImpact?.Invoke(this, new OnBulletImpactEventArgs
        {
            impactPosition = collision.GetContact(0).point
        });
        if(collision.gameObject.TryGetComponent(out IDamagable damagable))
        {
            if (damagePlayer)
            {
                //can damagePlayer
                damagable.TakeDamage(damageAmount, collision.GetContact(0).point); 
            }
            else
            {
                if (collision.transform.TryGetComponent(out ThirdPersonShooterController player))
                {
                    //this is the player
                    Destroy(gameObject);
                    return;
                }
                damagable.TakeDamage(damageAmount, collision.GetContact(0).point);
            }
        }
        Instantiate(_BulletImpactFX, collision.GetContact(0).point, Quaternion.identity);
        Destroy(gameObject);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);    
    }
}
