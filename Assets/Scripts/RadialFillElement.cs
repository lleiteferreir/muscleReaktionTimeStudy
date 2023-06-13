// Autor: LLF

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
//Addes effect to circle for radial fill
public class RadialFillElement : MonoBehaviour
{
    [SerializeField] private Image radialIndicatorUI = null;
    [SerializeField] public LightChanger _lightChanger;

    public GameObject muscleStrength;

    private int channel1;
    private int channel2;
    private int channel3;
    private int channel4;

    private float setUICh1;
    private float setUICh2;
    private float setUICh3;
    private float setUICh4;
    
    private void Update()
    {
        if (_lightChanger.startStudy)
        {
            //only to see if light is green
            if (_lightChanger.lightActive)
            {
                radialIndicatorUI.enabled = true;
                muscleStrength.SetActive(true);
                //checks which bodypart is used
                switch (_lightChanger.muscle)
                {
                    case "biceps":
                        radialIndicatorUI.fillAmount = setUICh1;
                        break;
                    case "triceps":
                        radialIndicatorUI.fillAmount = setUICh2;
                        break;
                    case "upper leg":
                        radialIndicatorUI.fillAmount = setUICh3;
                        break;
                    case "calf":
                        radialIndicatorUI.fillAmount = setUICh4;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                radialIndicatorUI.enabled = false;
                muscleStrength.SetActive(false);
            }
        }
        else
        {
            radialIndicatorUI.enabled = false;
            muscleStrength.SetActive(false);
        }
    }
    //setting data range for radial fill
    public void setData(String onDataRecieved)
    {
        splitConditions(onDataRecieved);
        //start with 0 if fill element should start with empty fill element
        setUICh1 = Range(channel1, 15000, 65535);
        setUICh2 = Range(channel2, 15000, 65535);
        setUICh3 = Range(channel3, 15000, 65535);
        setUICh4 = Range(channel4, 15000, 65535); 
    }
    //splits data for each ch (biceps, triceps, upperleg, calf)
    private void splitConditions(String onDataRecieved)
    {
        string[] conditionList = onDataRecieved.Split(";");
        channel1 = int.Parse(conditionList[0]);
        channel2 = int.Parse(conditionList[1]);
        channel3 = int.Parse(conditionList[2]);
        channel4 = int.Parse(conditionList[3]);
    }
    //sets value from 0 to 1
    public float Range(float val, int min, int max)
    {
        return (val - min) / (max - min);
    }
        
}
