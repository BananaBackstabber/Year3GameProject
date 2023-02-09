using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TImeAndHealthUI : MonoBehaviour
{

    public Slider slider;

    public void SetTimeGauge(int TimeGauge) 
    {
        slider.value = TimeGauge;
    }

    public void SetMaxPower(int TimeGauge) 
    {
        slider.maxValue = TimeGauge;
        slider.value = TimeGauge;
    }
   
    
}
