using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_up_Crystal : MonoBehaviour 
{
    // Start is called before the first frame update

    public Spawn_player Spawn;

    
    private void OnTriggerStay(Collider other)
    {
        // If player collides with the Time Crtystal them the rope and win box spawn in at the entery point 
        // AKA The Win codition has been met
        if(other.gameObject.tag == "Player") 
        {



            
            Debug.Log("WIN ACTIVE");
            Spawn.RunWin = true;
            Destroy(gameObject);
            Debug.Log(Spawn.RunWin);
        }
    }

  
}
