using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUnit : MonoBehaviour
{

    private float health;
    private float maxHealth;

    void Start()
    {
        this.health = 80;
        this.maxHealth = 100;
    }

    public void heal(float amm)
    {
        if(this.health + amm > this.maxHealth)
        {
            this.health = this.maxHealth;
        }
        else
        {
            this.health += amm;
        }
    }

    public void sustainDamage(float amm)
    {
        if(this.health - amm < 0)
        {
            this.health = 0;
        }
        else
        {
            this.health -= amm;
        }
    }

    public void replenish()
    {
        this.health = this.maxHealth;
    }

    public float getCurrentHealth()
    {
        return this.health;
    }

    public float getMaxHealth()
    {
        return this.maxHealth;
    }

    public void setMaxHealth(float amm)
    {
        this.maxHealth = amm;
    }

    public void setCurrentHealth(float amm)
    {
        if(amm < 0)
        {
            this.health = 0;
        }
        else if(amm > this.maxHealth)
        {
            this.health = maxHealth;
        }
        else
        {
            this.health = amm;
        }
    }
}
