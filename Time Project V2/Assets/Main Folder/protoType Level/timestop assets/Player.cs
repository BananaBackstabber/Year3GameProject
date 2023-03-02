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

    private float pDamage = 25;
    void Start()
    {
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        HealthBar = GameObject.FindGameObjectWithTag("UIHealth").GetComponent<HealthBar>();
        powersOn = GameObject.FindGameObjectWithTag("Power_selecter").GetComponent<Select_powers>();
        Current_health = maxPlayer_health;
        HealthBar.SetMaxHealth(maxPlayer_health);
        Time.timeScale = 0.4f;
        Invoke("Timenormal", 0.8f);
    }

    void Timenormal() 
    {
     // Debug.Log("NormalTime");
     Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(powersOn.slowIsClick == true) 
        {
            if (Input.GetMouseButtonDown(1) && timemanager.cooldown == false) //Stop Time when Q is pressed
            {

                toggletime();
            }
        }
        
      

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Current_health -= pDamage;
            HealthBar.SetPlayerHealth(Current_health);
            //Application.Quit();
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
            //Player_health = 200;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);


        }

        if (Input.GetKeyDown("k")) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

  

    void toggletime()
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
