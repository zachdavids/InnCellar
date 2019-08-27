using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController: MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float _maxHealth = 80;

    private float _currentHealth;
    public float currentHealth
    {
        get { return _currentHealth; }
    }

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Heal(float amount)
    {
        if (_currentHealth + amount > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        else
        {
            _currentHealth += amount;
        }
    }

    public void SustainDamage(float amount)
    {
        if (_currentHealth - amount < 0)
        {
            _currentHealth = 0;
        }
        else
        {
            _currentHealth -= amount;
        }
    }

    public void Replenish()
    {
        _currentHealth = _maxHealth;
    }

    public void SetCurrentHealth(float amount)
    {
        if (amount < 0)
        {
            _currentHealth = 0;
        }
        else if (amount > this._maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        else
        {
            _currentHealth = amount;
        }
    }
}
