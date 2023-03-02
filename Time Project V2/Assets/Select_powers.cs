using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_powers : MonoBehaviour
{
    public bool reverseIsClick;
    public bool slowIsClick;


    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void selectReverseTime() 
    {
        //Reverse Time is active when Right mouse button is clicked
        reverseIsClick = true;
        slowIsClick = false;
    
    }

    public void selectSlowTime() 
    {
        //Slow time is active when Right mouse button is clicked
        reverseIsClick = false;
        slowIsClick = true;
    }
}
