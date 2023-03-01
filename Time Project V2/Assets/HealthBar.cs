using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider hSlider;

    public Image HealthFill;

    public Gradient gradient;
    public void SetPlayerHealth(float BarHealth)
    {
        hSlider.value = BarHealth;

        HealthFill.color = gradient.Evaluate(hSlider.normalizedValue);
    }

    public void SetMaxHealth(float BarHealth)
    {
        hSlider.maxValue = BarHealth;
        hSlider.value = BarHealth;

        //HealthFill.color = gradient.Evaluate(1f);

    }
}
