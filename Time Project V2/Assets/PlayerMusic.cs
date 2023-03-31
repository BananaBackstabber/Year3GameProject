using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMusic : MonoBehaviour
{
    public MusicControl sound;


    // Start is called before the first frame update
    void Start()
    {
        sound = GameObject.FindGameObjectWithTag("Audio").GetComponent<MusicControl>();
        sound.CombatMusic();

    }
         


    
}
