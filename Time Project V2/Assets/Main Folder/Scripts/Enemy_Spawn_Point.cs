using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn_Point : MonoBehaviour
{

    public Spawn_Enemies_test Button;

    private int count = 1;

    // Update is called once per frame
    void Update()
    {
        if(Button.Button_pressed == true && count <= 1) 
        {
            Spawn();
        }
    }

    void Spawn() 
    {
        Debug.Log("Enemies are in");

       
        Instantiate(Button.TheEnemy, transform.position, transform.rotation);
       

        //Button.Button_pressed = false;

        count += 1;


    }
}
