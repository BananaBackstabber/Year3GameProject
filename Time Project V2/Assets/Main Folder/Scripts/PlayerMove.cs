using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerMove : MonoBehaviour
{
    private CharacterController Character_Controller;

    private Vector3 move_direction;

    public float speed = 5f;
    private float gravity = 20f;

    public float jump_Force = 10f;
    private float vertical_velocity;

    private void Awake()
    {
        Character_Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();
    }
    void MoveThePlayer()
    {
        //Axis.Horizontal and vertical are from the player helper script
        move_direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f,
                                     Input.GetAxis(Axis.VERTICAL));
        move_direction = transform.TransformDirection(move_direction);
        move_direction *= speed * Time.deltaTime;

        ApplyGravity();

        Character_Controller.Move(move_direction);

    }

    void ApplyGravity() 
    {
      
      vertical_velocity -= gravity * Time.deltaTime;

    //MoveToJump
    PlayerJump();
      
        //gravity while moving
        move_direction.y = vertical_velocity * Time.deltaTime;
    }

    void PlayerJump()
    {
        if(Character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            vertical_velocity = jump_Force;
        }
    }
}
