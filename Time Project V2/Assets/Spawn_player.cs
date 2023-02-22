using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_player : MonoBehaviour, IClick
{
    public GameObject Spawn_Object;
    public GameObject Spawn_rope;
   // public GameObject Win_box;

    public GameObject Spawn_Location;

    public GameObject TopCam;
    public GameObject Cursor;

    public Vector3 Vector;

    public bool Win_condition;

    public bool RunWin;

    public void Start()
    {
        //Win_condition = false;
    }

    public void Update()
    {
        
        if (RunWin == true) 
        {
            //Debug.Log(Win_condition);

            if(Win_condition == true) {
                ActivateWin();
            }
            


        }

       // Debug.Log("Win  = " + Win_condition);
        if (Input.GetKeyDown("k")) 
        {

            
            Debug.Log("ButtonDown");
            
            if (Win_condition == true) {
                Spawn_rope.SetActive(true);
                
                Instantiate(Spawn_rope, Spawn_Location.transform.position, Spawn_Location.transform.rotation);
                
            }
            
        }
    }
    public void onClickAction()
    {

      
        Instantiate(Spawn_Object, Spawn_Location.transform.position, Quaternion.identity);
        
        Cursor.SetActive(false);
        TopCam.SetActive(false);

        Win_condition = true;
        Debug.Log("SET BOX = " + Win_condition);

        // ActivateWin();
    }

   void DeactiveWin() 
    {
        //Spawn_rope.SetActive(false);
        //Win_box.SetActive(false);
    }

    public void ActivateWin() 
    {
        

        if (Win_condition == true) 
        {
            Debug.Log("SET BOX 2");
            Spawn_rope.SetActive(true);
           
            Instantiate(Spawn_rope, Spawn_Location.transform.position, Spawn_Location.transform.rotation);
            //Instantiate(Win_box, Spawn_Location.transform.position + Vector, Spawn_Location.transform.rotation);

            RunWin = false;
        }
        

    }
  
}
