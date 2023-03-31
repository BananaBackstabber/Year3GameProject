using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeUI : MonoBehaviour
{
    public Slider pSlider;
  
    public Image Powerfill;
    

    public void SetTimeGauge(float TimeGauge) 
    {
        pSlider.value = TimeGauge;
        
    }

    public void SetMaxPower(float TimeGauge) 
    {
        pSlider.maxValue = TimeGauge;
        pSlider.value = TimeGauge;
    }



}
