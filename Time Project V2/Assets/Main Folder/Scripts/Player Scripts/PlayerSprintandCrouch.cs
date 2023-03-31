using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintandCrouch : MonoBehaviour
{
    private PlayerMove PlayerMove;


    public float sprint_Speed = 10f;
    public float move_Speed = 5f;
    public float crouch_Speed = 2f;

    // set camera height when crouched
    private Transform look_Root;
    private float stand_Height = 1.6f;
    private float crouch_Height = 1f;

    private bool is_Crouching;

       void Awake()
    {
        PlayerMove = GetComponent<PlayerMove>();

        look_Root = transform.GetChild(0);
    }


    // Update is called once per frame
    void Update()
    {
        Sprint();

        Crouch();
    }

    void Sprint () 
    {
        //    IS leftShift key is down        and is not Crouching
        if(Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching)
        {
            PlayerMove.currentspeed = sprint_Speed;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching) {
            PlayerMove.currentspeed = move_Speed;
        }
    }

    void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            // is we are crouching, stand up
            if (is_Crouching)
            {
                look_Root.localPosition = new Vector3(0f, stand_Height, 0f);
                PlayerMove.currentspeed = move_Speed;

                is_Crouching = false;
            } else {
                // is we are  not crouching, crouch
                look_Root.localPosition = new Vector3(0f, crouch_Height, 0f);
                PlayerMove.currentspeed = crouch_Speed;

                is_Crouching = true;
            }
        }// if we can crouch

    }
}
