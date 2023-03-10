using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDeath : MonoBehaviour
{
    public float laserdamage;
    public int enemydamage;
    public Player playerhp;
    public Enemy_Health enemyhp;

    //Animator Variables
    public Animator animator;
   
    private float laserSpeed = 1f;

    private float uptime = 5f;
    private bool laseron = false;
    private void Awake()
    {
        playerhp = null;
    }

    private void Update()
    {
        Debug.Log("LASER..." + uptime + laseron);

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



        if (laseron == true)//Drains laser
        {
            uptime -= 2f * Time.deltaTime;
        }

        if(uptime <= 0f)//Disables Laser
        {
            uptime = 0f;
            animator.SetBool("isSensorTriggered", false);
            animator.SetFloat("laserSpeed", 0.5f);
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
           // CancelInvoke("Recharge");
        }
    }
    private void OnTriggerStay(Collider other)
    {

        if(playerhp == null)//should only read and GetComponent once
        {
            playerhp = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        if (other.gameObject.tag == "Player")//Player takes damages from laser
        { 
            playerhp.Current_health -= laserdamage;
        }

        if (other.gameObject.tag == "Hit")//enemy will take damage from laser
        {
            enemyhp = GameObject.FindGameObjectWithTag("Hit").GetComponent<Enemy_Health>();
            enemyhp.Health -= enemydamage;
        }
    }

    public void OnSensor() 
    {

        if (uptime == 5f) 
        {
            laseron = true;
            Debug.Log("LASER IS ON");
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
