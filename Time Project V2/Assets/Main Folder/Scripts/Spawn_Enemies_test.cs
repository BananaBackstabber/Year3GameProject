using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemies_test : MonoBehaviour
{
    [SerializeField] private bool openTrigger = false;

    public bool Button_pressed = false;

     public GameObject TheEnemy;

   // [SerializeField] public GameObject Spawn_Point;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Button_pressed = true;

            Debug.Log("ZONE");
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Spawn_starts");
                Button_pressed = true;

            }

        }


    }

 
}
