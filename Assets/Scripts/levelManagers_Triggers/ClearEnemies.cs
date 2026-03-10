using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class ClearEnemies : MonoBehaviour
{
    [Header("Enemies in room")]
    [SerializeField]List<Enemy> _enemyList;

    int _remainingEnemies;

    public UnityEvent OnRoomCleared;

    private void Start()
    {
        foreach (Enemy enemy in _enemyList)
        {
            enemy.OnEnemyDeath += Enemy_OnEnemyDeath;
        }
        _remainingEnemies = _enemyList.Count;
    }

    private void Enemy_OnEnemyDeath(object sender, System.EventArgs e)
    {
        Debug.Log("enemy Died");
        _remainingEnemies--;
        if (_remainingEnemies == 0)
        {
            Debug.Log("Room CLeared");
            OnRoomCleared.Invoke();
        }
    }
}
