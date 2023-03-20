using UnityEngine.Audio;
using System;
using UnityEngine;

public class audiomanager : MonoBehaviour
{

    public Sound[] sounds;

    public float slowingtime;
    public float timedrain;
    public float mintime;

    public TimeManager timemanager;

    // Start is called before the first frame update
    void Awake()
    {
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();

        foreach (Sound s in sounds) 
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.currentvolume;
        
            s.source.pitch = s.pitch;
            //sets the time drain variable to the current sounds pitch
            slowingtime = s.pitch;
            s.normalpitch = s.pitch;
        }

    }

    public void Play(string name) 
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (!timemanager.TimeIsSlow) 
        {
            //Restes the sound to its normal pitch setting after slow time is off
            slowingtime = s.normalpitch;
        }

        s.pitch = slowingtime;
        s.source.volume = s.currentvolume;
        s.source.pitch = s.pitch;
        s.source.Play();


    }

    private void Update()
    {
        SlowSound();

        if(timemanager.TimeIsSlow == false) 
        {
            Sound s = Array.Find(sounds, sound => sound.name == "Recharge Power Pick Up");
            slowingtime = s.normalpitch;
            s.pitch = slowingtime;
        }
    }


    void SlowSound() 
    {
        //Sound s = Array.Find(sounds, sound => sound.name == name);

        //when slow time is active this code should decreas ethe spped of the sound
        if (timemanager.TimeIsSlow == true)
        {
            slowingtime -= timedrain * Time.deltaTime; 

            if(slowingtime <= mintime) 
            {
                slowingtime = mintime;
            }

        }

    }

    public void TimeScaleSlow() 
    {
       // slowingtime = 0.3f;
    }

    public void TimeScaleNormal() 
    {
      
    }
}
