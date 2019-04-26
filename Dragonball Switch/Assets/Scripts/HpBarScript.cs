using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarScript : MonoBehaviour
{
    private float fillAmount;
    public Image content;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Text valueText;

    public float MaxValue { get; set; }
    public float Value
    {
        set
        {
            //Store everything before : into array
            string[] tmp = valueText.text.Split(':');
            //print out array and value
            valueText.text = tmp[0] + ": " + value;
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }


    private void Start()
    {
    }

    private void Update()
    {
        if (fillAmount != content.fillAmount)
        {
            HandleBar();
        } 
    }

    private void HandleBar()
    {
        content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
    }

    //Take current health and change that value between 0 and 1
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        
    }


    
}
