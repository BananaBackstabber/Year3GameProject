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

    public GameObject Player_UI;
    public GameObject Planning_UI1;
    public GameObject Planning_UI2;

    public Pick_up_Crystal TimeRelic;
    


    public Vector3 Vector;

    public bool Win_condition;

    public bool RunWin;

    public void Start()
    {
        //Clean up try to condense this down so all game objects are disable with one or two lines of code
        Player_UI = GameObject.FindGameObjectWithTag("UITime");
        Planning_UI1 = GameObject.FindGameObjectWithTag("Plan_UI");
        Planning_UI2 = GameObject.FindGameObjectWithTag("Plan_UI2");
       
        Invoke("disableUI", 0.001f);
        //Win_condition = false;
    }

    void disableUI() 
    {
        //Debug.Log("DisableUI IS A GO");
        Player_UI.SetActive(false);
    }
    public void Update()
    {
        

        if (RunWin == true) 
        {
            //when player picks up crystal 
            if(Win_condition == true)
            {
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

        // when entry point is clicked player will spawn at that point that is clicked
        Instantiate(Spawn_Object, Spawn_Location.transform.position, Quaternion.identity);
        
        //Turns the Top Down UI elements off
        Cursor.SetActive(false);
        TopCam.SetActive(false);
        Player_UI.SetActive(true);
       // Planning_UI1.SetActive(false);
       // Planning_UI2.SetActive(false);

        //Bool that makes it so the exit rope will spawn at the clicked entry point only
        Win_condition = true;
        //set the Time relic script to the spawn point the player has clicked on
        TimeRelic.Spawn = this.GetComponent<Spawn_player>();
    }

    public void ActivateWin() 
    {
        if (Win_condition == true) 
        {
            //Spawns and actives the rope component when Win codition = true
            Spawn_rope.SetActive(true);
            Instantiate(Spawn_rope, Spawn_Location.transform.position + Vector, Spawn_Location.transform.rotation);

            RunWin = false;
        }
        

    }
  
}
