using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Fastfoward : MonoBehaviour
{
    public float NoramlTime;
    public float SpeedTime;

    public TimeManager timemanager;
 
    public Material TimeMat;

    private bool toggle;

    private void Update()
    {
        if (Input.GetKeyDown("f") && timemanager.cooldown == false) 
        {
            toggletime();
        
        }

        if (Input.GetKey("v"))
        {
            Time.timeScale = NoramlTime;
            TimeMat.color = Color.black;
        }
    }
    void toggletime()
    {
        toggle = !toggle;

        if (toggle)
        {
            timemanager.isfast = true;

            Debug.Log("toggle On");
            if (timemanager.isfast == true)
            {
                Time.timeScale = SpeedTime;
                TimeMat.color = Color.green;

            }
            
            
        }
        else
        {
            Debug.Log("toggle off");
            Time.timeScale = NoramlTime;
            TimeMat.color = Color.black;
            timemanager.isfast = false;
        }

        if(timemanager.cooldown == true) 
        {
            Debug.Log("Cooldown");
            timemanager.isfast = false;

            Time.timeScale = NoramlTime;
            TimeMat.color = Color.black;
            

        }


    }

}
