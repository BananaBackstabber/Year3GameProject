using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController Character_Controller;

    private Vector3 move_direction;
    public float speed = 5f;
    public float gravity = 20f;

    void Awake() {
        Character_Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();
    }
    void MoveThePlayer() 
    {
        move_direction = new Vector3 (Input.GetAxis(Axis.HORIZONTAL), 0f,
                                     Input.GetAxis(Axis.VERTICAL));
        move_direction = transform.TransformDirection(move_direction);
        move_direction *= speed * Time.deltaTime;

        Character_Controller.Move(move_direction);

    }

    
}

