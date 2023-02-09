using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Linq;

public class TimeManager : MonoBehaviour
{
    //Access the Script for the UI 
    public TimeAndHealthUI UI;

    [SerializeField] private ForwardRendererData rendererData = null;
    [SerializeField] private string featureName = null;
    [SerializeField] private float transitionPeriod = 1;

    private bool transitions;
    private float startTime;

    public bool TimeIsSlow;

    public bool isRewinding = false;

    public bool isfast = false;

    //Time Guage Variables
    public bool cooldown = false;
    public float TimeGauge = 5f;
    public float SlowTimeDrain;
    public float RewindTimeDrain;
    public float TimePowersRecharge;
    //On screen effect
    public Material timeMat;
    //public Renderer Mrenderer;
    public Gradient gradient;
    private GradientColorKey[] colorKey;
    private GradientAlphaKey[] alphaKeys;

    private void Start()
    {
        //SlowTimeDrain *= Time.deltaTime;
        //TimePowersRecharge * Time.deltaTime;
        timeMat.color = Color.black;

        //Sets the UI Time Gauage bar to equal the value it is currently at
        UI.SetTimeGauge(TimeGauge);

        
    }
    public void ContinueTime()
    {
        TimeIsSlow = false;
        isRewinding = false;
        isfast = false;
        //Time Gauge starts to recharge
        

        //Screen effect is off
        timeMat.color = Color.black;
        foreach (var LocalKeywords in timeMat.shaderKeywords) 
        {
            Debug.Log("Local shader keyword" + timeMat.shaderKeywords + "Is currenly enabled");
        }

        var objects = FindObjectsOfType<TimeStop>();  //Find Every object with the Timebody Component
        for (var i = 0; i < objects.Length; i++)
        {
            objects[i].GetComponent<TimeStop>().ContinueTime(); //continue time in each of them
        }
   
    }
    private void Update()
    {
        //Debug.Log("TimeGuage = " + TimeGauge);
        //Debug.Log("Cooldown = " + cooldown);
        UI.SetTimeGauge(TimeGauge);

        if (TimeGauge <= 1.5) 
        {
            UI.fill.color = Color.red;
        
        }
        // If Time Gauge equals max then turn the bar green

        if (TimeGauge <= 4.9)
        {
            UI.fill.color = Color.blue;

        }

        if (TimeGauge == 5) 
        {

            UI.fill.color = Color.green;
        }
        // If cooldown is active turn the bar grey to show it is inactive

        if (cooldown == true) 
        {
            UI.fill.color = Color.grey;
        
        }

        if (TimeIsSlow == true) 
        {

            TimeGauge -= SlowTimeDrain * Time.deltaTime;
            // drain 1 form the variable every one second

        }

        if (isRewinding == true)
        {

            TimeGauge -= RewindTimeDrain * Time.deltaTime;
            // drain 1 form the variable every one second

        }

        if (isfast == true) 
        {
            TimeGauge -= SlowTimeDrain * Time.deltaTime;
        
        }

        //Cooldown actives player can not use time powers while cooldown is active
        if (TimeGauge <= 0) 
        {
            ContinueTime();
            cooldown = true;
        }

        //Cooldown reset player can use time powers again
        if (TimeGauge >= 5)
        {
            cooldown = false;
        }

        if (TimeIsSlow == false && isRewinding == false) 
        {
            TimeGauge += TimePowersRecharge * Time.deltaTime;
        }

        if(TimeGauge >= 5) 
        {
            TimeGauge = 5;
        }

       
  
    }
    public void StopTime()
    {

        TimeIsSlow = true;

        //Screen effect is on
        timeMat.color = Color.white;
    }
    private bool TryGetFeature(out ScriptableRendererFeature feature) 
    {
        feature = rendererData.rendererFeatures.Where((f) => f.name == featureName).FirstOrDefault();
        return feature != null;
    }

    private void Transition() 
    {
        if(TryGetFeature(out var feature)) { 
             
          // var blitFeature = feature as
        }
    }
}
