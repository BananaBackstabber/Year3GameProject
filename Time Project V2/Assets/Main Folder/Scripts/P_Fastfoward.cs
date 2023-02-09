using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Fastfoward : MonoBehaviour
{
    public float NoramlTime;
    public float SpeedTime;

    public TimeManager timemanager;
  

    public Material TimeMat;

    private void Update()
    {
        if (Input.GetKey("x")) 
        {
            Time.timeScale = SpeedTime;
            TimeMat.color = Color.green;
            timemanager.isfast = true;
        }

        if (Input.GetKey("v"))
        {
            Time.timeScale = NoramlTime;
            TimeMat.color = Color.black;
        }
    }
    
}
