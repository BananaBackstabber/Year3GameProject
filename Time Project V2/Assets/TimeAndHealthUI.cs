using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeAndHealthUI : MonoBehaviour
{

    public Slider slider;

    public Image fill;


    public void SetTimeGauge(float TimeGauge) 
    {
        slider.value = TimeGauge;
    }

    public void SetMaxPower(float TimeGauge) 
    {
        slider.maxValue = TimeGauge;
        slider.value = TimeGauge;
    }
   
   
   
}
