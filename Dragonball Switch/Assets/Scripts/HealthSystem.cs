using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    private int health;
    private int maxHealth = 100;
    public event EventHandler onHealthChange;

    public HealthSystem(int maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }
    public float GetHealthPercent()
    {
        return (float)health / maxHealth;
    }

    public void Damage(int damageAmmount)
    {
        health -= damageAmmount;
        if (health < 0) health = 0;
        if (onHealthChange != null) onHealthChange(this, EventArgs.Empty);
    }
    public void Heal(int healAmmount)
    {
        health += healAmmount;
        if (health > maxHealth) health = maxHealth;
        if (onHealthChange != null) onHealthChange(this, EventArgs.Empty);
    }
}
