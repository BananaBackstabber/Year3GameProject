using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    private TimeManager timemanager;
    public Select_powers powersOn;
    //public GrayscaleLayers Grayscale;
    // Start is called before the first frame update

    private bool toggle;

    //Health VARIABLES
    public float maxPlayer_health;
    public float Current_health;
    public HealthBar HealthBar;

    //Bleed out state
     public Animator animator;
     public Animation BleedAnim;

    private float pDamage = 25;
    void Start()
    {
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        HealthBar = GameObject.FindGameObjectWithTag("UIHealth").GetComponent<HealthBar>();
        powersOn = GameObject.FindGameObjectWithTag("Power_selecter").GetComponent<Select_powers>();
        //animator = GetComponent<Animator>();
        Current_health = maxPlayer_health;
        HealthBar.SetMaxHealth(maxPlayer_health);
        Time.timeScale = 0.4f;
        Invoke("Timenormal", 1f);
    }

    void Timenormal() 
    {
     // This fuction returns time to normals after a few seconds of slow time at the beginning of the player spawn
     Time.timeScale = 1f;
        Debug.Log("Timenormal");
    }

    // Update is called once per frame
    void Update()
    {
        if(powersOn.slowIsClick == true && Input.GetMouseButtonDown(1) && timemanager.cooldown == false) 
        {
                Toggletime();
        }
        
      

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            //Current_health -= pDamage;
            HealthBar.SetPlayerHealth(Current_health);
            //Application.Quit();
        }

        if(timemanager.isRewinding == true) 
        {
            HealthBar.SetPlayerHealth(Current_health);
        }

        //CODE TO RESET ROOM FOR DEBUG TESTING 
       /* if (Input.GetKeyDown("6"))
        {
            Debug.Log("ESCAPE");
            SceneManager.LoadScene(1);
        }*/

        //HEALTH VALUES

        
        /*if(Player_health <= 100) 
        {
            HealthBar.HealthFill.color = Color.cyan;
        }
        if (Player_health <= 50) 
        {
            
            HealthBar.HealthFill.color = Color.yellow;
        }

        if(Player_health <= 25)
        {
            HealthBar.HealthFill.color = Color.red;
        }*/


        if(Current_health <= 0) 
        {

          
            if(timemanager.isRewinding == false) 
            {
                Time.timeScale = 0.4f;
                Debug.Log("BLEEDINHOUT");
                animator.SetBool("IsBleeding", true);
                animator.applyRootMotion = false;
                animator.SetFloat("SpeedM", 0.5f);
                Invoke("reload", 2f);
                CancelInvoke("ReverseDeath");
            }
            else if(timemanager.isRewinding == true) 
            {
                Debug.Log("ISREVERSING");
                animator.SetFloat("SpeedM", -0.5f);
                CancelInvoke("reload");
                Invoke("ReverseDeath", 2f);
            
            }
            

        }

        if (Input.GetKeyDown("t")) 
        {
            //Debug.Log(Current_health);
            //Debug.Log(Current_health);
            Current_health -= pDamage;
        }

    }

    void reload() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ReverseDeath()
    {
        Debug.Log("ISNOTDEAD");
        animator.SetBool("IsBleeding", false);
        animator.applyRootMotion = true;
    }
    void Toggletime()
    {
        toggle = !toggle;

        if (toggle) 
        {
            timemanager.StopTime();
        }
        else 
        {
            timemanager.ContinueTime();
        }
        
    
    }
}
