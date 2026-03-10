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

    private void Enemy_OnDeath(object sender, System.EventArgs e)
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
