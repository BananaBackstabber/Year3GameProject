using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public TimeManager timemanager;


    [SerializeField]
    private Transform playerRoot, lookRoot;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool can_Unlock = true;

    [SerializeField]
    private float sensivity = 5f;

    [SerializeField]
    private int smooth_Steps = 10;

    //[SerializeField]
    //private float smooth_Weight = 0.4f;

    [SerializeField]
    private float roll_Angle = 10f;

    [SerializeField]
    private float roll_Speed = 3f; 

    [SerializeField]
    private Vector2 default_Look_Limits = new Vector2(-70, 80f);

    private Vector2 look_Angles;

    private Vector2 current_Mouse_Look;
    private Vector2 smooth_Move;

    private float current_Roll_Angle;

    private int last_Look_Frame;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
       
    }

    // Update is called once per frame
    void Update()
    {

        Cursor.lockState = CursorLockMode.Locked;
        LockAndUnlockCursor();
        // when mouse is locked you can look around
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }

        // If the reverse time power is active you can not look around
        if (timemanager.isRewinding == false)
        {
           

        }
        else 
        {
            //Code here need to either reset the mouse rotation after player has rewinded
            //Or record the mouse rotation when rewinding and replace when player has stopped rewinding
        }
    }

    void LockAndUnlockCursor() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { // when escape is pressed and the cursor is locked, unlock the cursor.
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;

            } else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                   }
        }
    } 
    void LookAround() // rotate mouse 
    {
        current_Mouse_Look = new Vector2(
            Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));

        look_Angles.x += current_Mouse_Look.x * sensivity * (invert ? 1f : -1f);
        look_Angles.y += current_Mouse_Look.y * sensivity;

        //              Clamp will not allow the number to go below x or above y value
        look_Angles.x = Mathf.Clamp(look_Angles.x, default_Look_Limits.x, default_Look_Limits.y);

        current_Roll_Angle =
             Mathf.Lerp(current_Roll_Angle, Input.GetAxisRaw(MouseAxis.MOUSE_X)
                        * roll_Angle, Time.deltaTime * roll_Speed);

        lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, current_Roll_Angle);
        playerRoot.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f); 

    } // look around 
}  //CLASS 













