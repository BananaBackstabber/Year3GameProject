using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDeath : MonoBehaviour
{
    public float laserdamage;
    public int enemydamage;
    public Player playerhp;
    public Enemy_Health enemyhp;

    private float nspeed = 2f;

    //Animator Variables
    public Animator animator;
    public TimeManager timemanager;
   
    private float laserSpeed = 1f;

    private float uptime = 5f;
    private bool laseron = false;

    private bool wallstop = false;
    private void Awake()
    {
        playerhp = null;
        enemyhp = null;
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
    }

    private void Update()
    {
        //Debug.Log("LASER..." + uptime + laseron);

        if (Input.GetKeyDown("t")) 
        {
            Debug.Log("tut");
            animator.SetBool("isSensorTriggered", true);
        }

        if (Input.GetKeyDown("u"))
        {
            Debug.Log("UM");
            animator.SetBool("isSensorTriggered", false);
        }

        if(timemanager.TimeIsSlow == true) 
        {
            animator.SetFloat("laserSpeed", 0.25f);
        }
        else if (timemanager.isRewinding == true || wallstop == true) 
        {
            animator.SetFloat("laserSpeed", 0f);
        }
        else if(wallstop == false) 
        {
            animator.SetFloat("laserSpeed", nspeed);
        }

        if (laseron == true && timemanager.TimeIsSlow == false
            && timemanager.isRewinding == false)//Drains laser
        {
            uptime -= 2f * Time.deltaTime;
        }

        if(uptime <= 0f)//Disables Laser
        {
            uptime = 0f;
            animator.SetBool("isSensorTriggered", false);
            laseron = false;

            //Invoke("Recharge", 2f);
        }

        if(laseron == false) 
        {
            if (uptime < 5f)//...Untill float varaible hits 5
            {
                uptime += 1f * Time.deltaTime;
                
            }
        }

        if (uptime > 5f)//stops the laser from over recharging  
        {
            uptime = 5f;
            wallstop = false;
            // CancelInvoke("Recharge");
        }
    }
    private void OnTriggerStay(Collider other)
    {

        if(playerhp == null && other.gameObject.tag == "Player")//should only read and GetComponent once
        {
            playerhp = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        if (other.gameObject.tag == "Player")//Player takes damages from laser
        {
            if (!timemanager.isRewinding) 
            {
            playerhp.Current_health -= laserdamage;
            }
            
        }

        if (other.gameObject.tag == "Hit")//enemy will take damage from laser
        {
            Debug.Log("RAY HIT");
          
            enemyhp = other.gameObject.GetComponent<Enemy_Health>();
            enemyhp.Health -= enemydamage;
        }

        if(other.gameObject.layer == 8) 
        {
            wallstop = true;
        }
        

    }
    


    public void OnSensor() 
    {

        if (uptime == 5f) 
        {
            laseron = true;
          //  Debug.Log("LASER IS ON");
            animator.SetFloat("laserSpeed", 2f);
            animator.SetBool("isSensorTriggered", true);
            

        }
        
    }

    void Recharge() 
    {

        if (uptime < 5f)//...Untill float varaible hits 5
        {
            uptime += 1f * Time.deltaTime;
        }

    }
}
