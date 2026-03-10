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

    const float _xOffsetMin = -1;
    const float _xOffsetMax = 1;
    const float _yOffsetMin = -1;
    const float _yOffsetMax = 1;
    const float _zOffsetMin = -1;
    const float _zOffsetMax = 1;

    float _lerpTimer = 0;
    Vector3 startPos;
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
                    if (!_movingStarted)
                    {
                        _movingStarted = true;
                        finalPos = transform.position + GetRandomMoveDirection();
                        startPos = transform.position;
                    }
                    if (_lerpTimer < 1)
                    {
                        _rigidBody.MovePosition(Vector3.Lerp(startPos, finalPos, _lerpTimer));
                        _lerpTimer += Time.deltaTime;
                    }
                    else
                    {
                        _lerpTimer = 0;
                        transform.position = finalPos;
                        _movingStarted = false;
                    }
                break;
        }
    }

    private Vector3 GetRandomMoveDirection()
    {
            //move the enemy

        float moveX = 0, moveY =0,moveZ = 0;
        Vector3 moveDir = Vector3.zero;
        Vector3 halfExtends = new Vector3(0.5f, 0.5f, 0.5f);

        if(!Physics.BoxCast(transform.position + new Vector3(0,0,1f),halfExtends,Vector3.forward))
        {
            moveZ = UnityEngine.Random.Range(0, _zOffsetMax);
            if (!Physics.BoxCast(transform.position - new Vector3(0, 0, 1f), halfExtends, Vector3.back))
            {
                moveZ = UnityEngine.Random.Range(_zOffsetMin, _zOffsetMax);
            }
        }
        if (!Physics.BoxCast(transform.position + new Vector3(0, 1, 0f), halfExtends, Vector3.up))
        {
            moveZ = UnityEngine.Random.Range(0, _zOffsetMax);
            if (!Physics.BoxCast(transform.position - new Vector3(0, 1, 0), halfExtends, Vector3.down))
            {
                moveY = UnityEngine.Random.Range(_yOffsetMin, _yOffsetMax);
            }
        }
        if (!Physics.BoxCast(transform.position + new Vector3(1,0, 0f), halfExtends, Vector3.right))
        {
            moveZ = UnityEngine.Random.Range(0, _zOffsetMax);
            if (!Physics.BoxCast(transform.position - new Vector3(1, 0, 0), halfExtends, Vector3.left))
            {
                moveX = UnityEngine.Random.Range(_yOffsetMin, _yOffsetMax);
            }
        }
        moveDir = new Vector3(moveX, moveY, moveZ);

        return moveDir;

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
