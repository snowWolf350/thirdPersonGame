using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class ClearEnemies : MonoBehaviour
{
    [Header("Enemies in room")]
    [SerializeField]List<Enemy> _enemyList;

    int _remainingEnemies = 0;

    public UnityEvent OnRoomCleared;

    private void OnEnable()
    {
        foreach (Enemy enemy in _enemyList)
        {
            enemy.GetHealthSystem().onDeath += Enemy_OnDeath;
        }
        _remainingEnemies = _enemyList.Count;
    }

    private void Start()
    {
        ThirdPersonShooterController.OnPlayerDeath += ThirdPersonShooterController_OnPlayerDeath;
    }

    private void ThirdPersonShooterController_OnPlayerDeath(object sender, System.EventArgs e)
    {
        SetEnemyStateIdle();
    }

    private void Enemy_OnDeath(object sender, System.EventArgs e)
    {
        _remainingEnemies--;
        if (_remainingEnemies == 0)
        {
            OnRoomCleared.Invoke();
        }
    }

    void SetEnemyStateIdle()
    {
        foreach(Enemy enemy in _enemyList)
        {
            enemy.SetIdleState();
        }
    }
}
