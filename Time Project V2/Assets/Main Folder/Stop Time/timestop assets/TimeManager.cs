using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Linq;

public class TimeManager : MonoBehaviour
{


    [SerializeField] private ForwardRendererData rendererData = null;
    [SerializeField] private string featureName = null;
    [SerializeField] private float transitionPeriod = 1;

    private bool transitions;
    private float startTime;

    public bool TimeIsStopped;

    //On screen effect
    public Material timeMat;
    //public Renderer Mrenderer;


   
    private void Start()
    {

        timeMat.color = Color.black;
        
    }
    public void ContinueTime()
    {
        TimeIsStopped = false;

        //Transition();

        //gameObject.GetComponent<Renderer>().material.SetFloat("_Intensity", 1);
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
    public void StopTime()
    {
       
        TimeIsStopped = true;

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
