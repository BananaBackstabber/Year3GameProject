using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    private TimeManager timemanager;
    //public GrayscaleLayers Grayscale;
    // Start is called before the first frame update

    private bool toggle;
  

    void Start()
    {
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();

       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && timemanager.cooldown == false) //Stop Time when Q is pressed
        {

            toggletime();
        }
        if(Input.GetKeyDown(KeyCode.E) && timemanager.TimeIsSlow)  //Continue Time when E is pressed
        {
            timemanager.ContinueTime();
            //Grayscale.enabled = false;
            //Debug.Log("Timeoff");

        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
           
            Application.Quit();
        }

        if (Input.GetKeyDown("6"))
        {
            Debug.Log("ESCAPE");
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown("7"))
        {
            Debug.Log("ESCAPE");
            SceneManager.LoadScene(0);
        }

    }

    void Retry()
    {

      


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
