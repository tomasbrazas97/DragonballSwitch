﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarScript : MonoBehaviour
{
    private HealthSystem healthSystem;

    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
        healthSystem.onHealthChange += HealthSystem_OnHealthChange;
    }
    private void HealthSystem_OnHealthChange (object sender, System.EventArgs e)
    {
        transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    }

    /*private void Update()
{

    transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1);   //Innefficient way of updating HP Bars.
}*/
}