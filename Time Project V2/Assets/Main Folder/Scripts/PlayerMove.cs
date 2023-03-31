using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerMove : MonoBehaviour
{
    private CharacterController Character_Controller;

    private Vector3 move_direction;

    public float setspeed;
    public float currentspeed;

    private float gravity = 20f;
    public float max_gravity;

    public float jump_Force = 10f;
    private float vertical_velocity;

    public TimeManager Rewind;
    private void Awake()
    {
        Character_Controller = GetComponent<CharacterController>();
        Rewind = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();

        //Debug.Log("Vertical speed = " + vertical_velocity);
    }
    void MoveThePlayer()
    {

        

        if (Rewind.isRewinding == true) 
        {
            //Debug.Log("N/G");
            NoGraivity();

        }
        else if (Rewind.isRewinding == false) 
        {

            

            //Axis.Horizontal and vertical are from the player helper script
            move_direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f,
                                         Input.GetAxis(Axis.VERTICAL));
            move_direction = transform.TransformDirection(move_direction);
            move_direction *= currentspeed * Time.deltaTime;

            ApplyGravity();
            Character_Controller.Move(move_direction);
        }

        

    }

    void ApplyGravity() 
    {
        if (Character_Controller.isGrounded)
        { 

           vertical_velocity -= gravity * Time.deltaTime;

            //MoveToJump
            PlayerJump();
        }
      else 
        {
            //Is calculates the gravity fall speed
            vertical_velocity -= gravity * Time.deltaTime;
        }


        //gravity while moving
        move_direction.y = vertical_velocity * Time.deltaTime;


        if(vertical_velocity <= max_gravity) 
        {
            vertical_velocity = max_gravity;
        }

    }

    void NoGraivity() 
    {
        vertical_velocity = 0;
    }

    void PlayerJump()
    {
        if(Character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            vertical_velocity = jump_Force;
        }
    }
}
