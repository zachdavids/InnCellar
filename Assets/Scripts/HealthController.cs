using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController: MonoBehaviour
{
    [Header("Health Settings")]
    public float m_MaxHealth = 80;

    private float m_CurrentHealth;

    public float GetCurrentHealth() { return m_CurrentHealth; }

    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
    }

    public void Heal(float amount)
    {
        if (m_CurrentHealth + amount > m_MaxHealth)
        {
            m_CurrentHealth = m_MaxHealth;
        }
        else
        {
            m_CurrentHealth += amount;
        }
    }

    public void SustainDamage(float amount)
    {
        if (m_CurrentHealth - amount < 0)
        {
            m_CurrentHealth = 0;
        }
        else
        {
            m_CurrentHealth -= amount;
        }
    }

    public void Replenish()
    {
        m_CurrentHealth = m_MaxHealth;
    }

    public void SetCurrentHealth(float amount)
    {
        if (amount < 0)
        {
            m_CurrentHealth = 0;
        }
        else if (amount > this.m_MaxHealth)
        {
            m_CurrentHealth = m_MaxHealth;
        }
        else
        {
            m_CurrentHealth = amount;
        }
    }
}
