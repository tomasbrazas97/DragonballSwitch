using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnergyScript : MonoBehaviour
{
    private int energy;
    private int maxEnergy = 100;
    public event EventHandler onEnergyhange;

    public EnergyScript(int maxEnergy)
    {
        this.maxEnergy = maxEnergy;
        energy = maxHealth;
    }

    public int GetEnergy()
    {
        return energy;
    }
    public float GetEnergyPercent()
    {
        return (float)energy / maxEnergy;
    }

    public void Spend(int damageAmmount)
    {
        energy -= damageAmmount;
        if (energy < 0) energy = 0;
        if (onEnergyhange != null) onEnergyhange(this, EventArgs.Empty);
    }
    public void Charge(int healAmmount)
    {
        energy += healAmmount;
        if (energy > maxEnergy) energy = maxEnergy;
        if (onEnergyhange != null) onEnergyhange(this, EventArgs.Empty);
    }
}