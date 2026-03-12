using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour,IHasProgress,IDamagable
{
    HealthSystem _healthSystem;

    [SerializeField] Transform _playerTransform;
    [SerializeField] Transform _enemyBullet;
    [SerializeField] Transform _enemyBulletSpawn;

    float _shootDelayTimer = 0;
    float _shootDelayTimerMax;
    float _shootDelayMin = 1;
    float _shootDelayMax = 3;
    float _playerHeightOffset = .1f;

    const float _moveOffsetMin = -1;
    const float _moveOffsetMax = 1;

    Vector3 finalPos;
    bool _movingStarted = false;

    Rigidbody _rigidBody;

    [Serializable]
    public enum enemyState
    {
        idle,
        attack,
    }

    enemyState _currentEnemyState;

    public event EventHandler<IHasProgress.onProgressChangedEventArgs> onProgressChanged;

    private void Awake()
    {
        _healthSystem = new HealthSystem(100);
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _playerTransform = GlobalReferences.Instance.PlayerTransform;
        onProgressChanged?.Invoke(this, new IHasProgress.onProgressChangedEventArgs
        {
            progressNormalized = 1
        });

        _currentEnemyState = enemyState.idle;
        _shootDelayTimerMax= UnityEngine.Random.Range(_shootDelayMin, _shootDelayMax);

        _healthSystem.onDeath += _healthSystem_onDeath;
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

    private void FixedUpdate()
    {
        if (_currentEnemyState == enemyState.attack)
        {
            HandleMovement();
        }
    }
    void HandleMovement()
    {
        if (!_movingStarted)
        {
            _movingStarted = true;
            finalPos = _rigidBody.position + GetRandomMoveDirection();
        }

        float speed = 1.5f;

        Vector3 newPos = Vector3.MoveTowards(
            _rigidBody.position,
            finalPos,
            speed * Time.fixedDeltaTime
        );

        _rigidBody.MovePosition(newPos);

        if (Vector3.Distance(_rigidBody.position, finalPos) < 0.05f)
        {
            _movingStarted = false;
        }
    }

    private Vector3 GetRandomMoveDirection()
    {
        float moveX = UnityEngine.Random.Range(_moveOffsetMin, _moveOffsetMax);
        float moveZ = UnityEngine.Random.Range(_moveOffsetMin, _moveOffsetMax);

        return new Vector3(moveX, 0f, moveZ);
    }

    void shoot()
    {
        Vector3 aimDir = (_playerTransform.position - transform.position).normalized;
        aimDir.y += _playerHeightOffset;
        transform.rotation = Quaternion.LookRotation(aimDir, Vector3.up);

        _shootDelayTimer += Time.deltaTime;
        if (_shootDelayTimer > _shootDelayTimerMax)
        {
            _shootDelayTimer = 0;
            Instantiate(_enemyBullet, _enemyBulletSpawn.position, Quaternion.LookRotation(aimDir));
        }
    }

    public void SetIdleState()
    {
        _currentEnemyState = enemyState.idle;
    }
    public void SetAttackState()
    {
        _currentEnemyState = enemyState.attack;
    }
    private void _healthSystem_onDeath(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damageAmount, Vector3 damagePoint)
    {
        _healthSystem.TakeDamage(damageAmount);

        onProgressChanged?.Invoke(this, new IHasProgress.onProgressChangedEventArgs
        {
            progressNormalized = _healthSystem.GetHealthNormalized()
        });
    }

    public HealthSystem GetHealthSystem()
    {
        return _healthSystem;
    }
}
