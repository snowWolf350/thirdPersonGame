using System;
using UnityEngine;

public class HealthSystem
{
    int _maxHealth;
    int _currentHealth;

    public event EventHandler OnDamange;
    public event EventHandler onDeath;

    public HealthSystem(int health)
    {
        _maxHealth = health;
        _currentHealth = health;
    }

    public float GetHealthNormalized()
    {
        return (float)_currentHealth / _maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;
        OnDamange?.Invoke(this, EventArgs.Empty);
        if (_currentHealth <= 0)
        {
            //this died
            onDeath?.Invoke(this, EventArgs.Empty);
        }
    }
}
