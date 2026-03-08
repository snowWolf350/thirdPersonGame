using System;
using UnityEngine;

public class Enemy : MonoBehaviour,IHasProgress
{
    int _enemyHealthMax = 100;
    int _enemyHealth;

    SphereCollider _sphereCollider;

    [SerializeField] Transform _playerTransform;
    [SerializeField] Transform _enemyBullet;
    [SerializeField] Transform _enemyBulletSpawn;

    float _shootDelay = 0;
    float _shootDelayMax = 4;
    float _playerHeightOffset = .1f;

    public event EventHandler OnEnemyDeath;

    [Serializable]
    public enum enemyState
    {
        idle,
        attack,
    }

    enemyState _currentEnemyState;

    public event EventHandler<IHasProgress.onProgressChangedEventArgs> onProgressChanged;

    private void Start()
    {
        _enemyHealth = _enemyHealthMax;
        _sphereCollider = GetComponent<SphereCollider>();
        onProgressChanged?.Invoke(this, new IHasProgress.onProgressChangedEventArgs
        {
            progressNormalized = 1
        });

        _currentEnemyState = enemyState.idle;
    }

    private void Update()
    {
        switch( _currentEnemyState )
        {
            case enemyState.idle:
                break;
            case enemyState.attack:
                shoot();
                break;
        }
    }

    void shoot()
    {
        Vector3 aimDir = (_playerTransform.position - transform.position).normalized;
        aimDir.y += _playerHeightOffset;
        transform.rotation = Quaternion.LookRotation(aimDir, Vector3.up);

        _shootDelay += Time.deltaTime;
        if (_shootDelay > _shootDelayMax)
        {
            _shootDelay = 0;
            Instantiate(_enemyBullet, _enemyBulletSpawn.position, Quaternion.LookRotation(aimDir));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") == false) return;
    }

    public void SetIdleState()
    {
        _currentEnemyState = enemyState.idle;
    }
    public void SetAttackState()
    {
        _currentEnemyState = enemyState.attack;
    }

    public void EnemyDamaged(int damageAmount)
    {
        _enemyHealth -= damageAmount;
        if (_enemyHealth <= 0)
        {
            OnEnemyDeath?.Invoke(this,EventArgs.Empty);
            Destroy(gameObject);
        }

        onProgressChanged?.Invoke(this, new IHasProgress.onProgressChangedEventArgs
        {
            progressNormalized = (float)_enemyHealth / _enemyHealthMax
        });
    }
}
