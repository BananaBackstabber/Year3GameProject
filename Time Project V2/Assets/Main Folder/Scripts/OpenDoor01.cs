using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OpenDoor01 : MonoBehaviour
{
    [SerializeField] private Animator theDoor01 = null;
    [SerializeField] private Animator theDoor02 = null;

    [SerializeField] private bool openTrigger = false;

    

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) 
        {

            if (openTrigger)
            {

                theDoor01.Play("Door one open", 0, 0.0f);
                theDoor02.Play("Door one open02", 0, 0.0f);

            }
        
        }

            
    }
}
