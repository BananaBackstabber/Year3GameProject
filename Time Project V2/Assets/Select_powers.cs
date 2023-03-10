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

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void selectReverseTime() 
    {
        //Reverse Time is active when Right mouse button is clicked
        isreversecurrent = true;
        isslowcurrent = false;
        TimeReverseImage.enabled = true;
        TimeSlowImage.enabled = false;

    }

    public void selectSlowTime() 
    {
        //Slow time is active when Right mouse button is clicked
        isreversecurrent = false;
        isslowcurrent = true;

        TimeReverseImage.enabled = false;
        TimeSlowImage.enabled = true;
    }
}
