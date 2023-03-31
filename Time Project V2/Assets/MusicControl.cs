using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicControl : MonoBehaviour
{

    private audiomanager sound;

    private GameObject playerspawned;

    public AudioSource playermusic;
    public AudioSource planmusic;

    public TimeManager timemanager;

    public float speeddrain;
    public float normalspeed;
    private void Start()
    {
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        sound = gameObject.GetComponent<audiomanager>();

        planmusic.enabled = true;
    }

    public void PlanMusic() 
    {
        sound.Play("Plan_music");
       
    }
    public void Update()
    {
       // Debug.Log(Time.timeScale);
        if(playermusic.enabled == true)
        {
            if (Time.timeScale == 0.4f)
            {
                playermusic.pitch = 0.6f;
            }
            else if (timemanager.TimeIsSlow == true)
            {//Slows the music down
                playermusic.pitch -= speeddrain * Time.deltaTime;

                if (playermusic.pitch <= 0.5f)
                {//Caps the slow down pitch
                    playermusic.pitch = 0.5f;
                }
            }
            else if (timemanager.isRewinding == true)
            {// This reverses the music pitch
                playermusic.pitch = -0.8f;
            }
            else
            {//Returns the music to normal pitch
                playermusic.pitch = normalspeed;
            }
        }
        
        
        
    }
    public void CombatMusic() 
    {
        planmusic.enabled = false;
        playermusic.enabled = true;


    }



    
}
