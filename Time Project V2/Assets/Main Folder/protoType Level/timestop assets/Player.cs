using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    private TimeManager timemanager;
    public Select_powers currentpower;
    //public GrayscaleLayers Grayscale;
    // Start is called before the first frame update

    //Power selection
    private bool toggle;
    private bool powertoggle;
    //public Select_powers currentpower;

    //Health VARIABLES
    public float maxPlayer_health;
    public float Current_health;
    public HealthBar HealthBar;
    //public LaserDeath Laser;

    //Bleed out state
     public Animator animator;
     public Animation BleedAnim;
     private float BleedSpeed = 1;
     public PlayerMove Movement;
     
    //DEBUG TESTING STUFF
    private float pDamage = 25;
    void Start()
    {
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        HealthBar = GameObject.FindGameObjectWithTag("UIHealth").GetComponent<HealthBar>();
        currentpower = GameObject.FindGameObjectWithTag("Power_selecter").GetComponent<Select_powers>();
       // Laser = GameObject.FindGameObjectWithTag("Laser").GetComponent<LaserDeath>();
        Movement = gameObject.GetComponent<PlayerMove>();
        //animator = GetComponent<Animator>();
        Current_health = maxPlayer_health;
        HealthBar.SetMaxHealth(maxPlayer_health);
        Time.timeScale = 0.4f;
        Invoke("Timenormal", 2f);
    }

    void Timenormal() 
    {
     // This fuction returns time to normals after a few seconds of slow time at the beginning of the player spawn
     Time.timeScale = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {

        HealthBar.SetPlayerHealth(Current_health);

        if (currentpower.isslowcurrent == true && Input.GetMouseButtonDown(1) && timemanager.cooldown == false) 
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


        //Switching powers code, takes from the power_selecter obj to switch between powers

        if (Input.GetKeyDown("q") && timemanager.TimeIsSlow == false
            && timemanager.isRewinding == false)//If reversetime is not active and slow time is active whwnq is pressed...
        {
            TogglePowers();
        }
        //HEALTH VALUES, testing what works


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

      
        if (Current_health <= 0) 
        {

          
            if(timemanager.isRewinding == false) 
            {
                Time.timeScale = 0.4f;
      
                animator.SetBool("IsBleeding", true);
                animator.applyRootMotion = false;
                animator.SetFloat("SpeedM", 0.5f);
                Movement.currentspeed = BleedSpeed;
                Invoke("reload", 2f);
                CancelInvoke("ReverseDeath");
            }
            else if(timemanager.isRewinding == true) 
            {
                Debug.Log("ISREVERSING");
                animator.SetFloat("SpeedM", -0.5f);
                Movement.currentspeed = Movement.setspeed;
                CancelInvoke("reload");
                Invoke("ReverseDeath", 2f);
            
            }
            

        }

        //FOR DEBUG TESTING ONLY
        if (Input.GetKeyDown("t")) 
        {
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

    void TogglePowers() 
    {
        //Toggle between reverse and slow time when Q is pressed

        powertoggle = !powertoggle;

        if (powertoggle)
        {
            currentpower.selectReverseTime();
        }
        else
        {
            currentpower.selectSlowTime();
        }

    }
}
