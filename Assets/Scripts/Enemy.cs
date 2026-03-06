using System;
using UnityEngine;

public class Enemy : MonoBehaviour,IHasProgress
{
    int enemyHealthMax = 100;
    int enemyHealth;

    public event EventHandler<IHasProgress.onProgressChangedEventArgs> onProgressChanged;

    private void Start()
    {
        enemyHealth = enemyHealthMax;
        onProgressChanged?.Invoke(this, new IHasProgress.onProgressChangedEventArgs
        {
            progressNormalized = 1
        });
    }

    public void EnemyDamaged(int damageAmount)
    {
        enemyHealth -= damageAmount;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }

        onProgressChanged?.Invoke(this, new IHasProgress.onProgressChangedEventArgs
        {
            progressNormalized = (float)enemyHealth / enemyHealthMax
        });
    }
}
