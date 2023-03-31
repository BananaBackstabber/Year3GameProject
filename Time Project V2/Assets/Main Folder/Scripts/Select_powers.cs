using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Select_powers : MonoBehaviour
{
    public bool isreversecurrent;
    public bool isslowcurrent;


    public Image TimeSlowImage;
    
    public Image TimeReverseImage;
    
   public void selectReverseTime() 
    {
        //Reverse Time is active when Right mouse button is clicked
        isreversecurrent = true;
        isslowcurrent = false;

        //Image Vars
        TimeReverseImage.enabled = true;
        TimeSlowImage.enabled = false;

    }

    public void selectSlowTime() 
    {
        //Slow time is active when Right mouse button is clicked
        isreversecurrent = false;
        isslowcurrent = true;

        //Image Vars
        TimeReverseImage.enabled = false;
        TimeSlowImage.enabled = true;
    }
}
