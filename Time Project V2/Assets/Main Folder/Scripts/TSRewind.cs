using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSRewind : MonoBehaviour
{

    public PRewind player_rewind;

    public float TimeBeforeAffected; //The time after the object spawns until it will be affected by the timestop(for projectiles etc)
    private TimeManager timemanager;
    private Rigidbody rb;
    private Vector3 recordedVelocity;
    private float recordedMagnitude;
    private float TimeBeforeAffectedTimer;
    private bool CanBeAffected;
    public bool IsStopped;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        TimeBeforeAffectedTimer = TimeBeforeAffected;
    }

    // Update is called once per frame
    void Update()
    {
       

       // Debug.Log("Object is stopped = " + rb.isKinematic);

        TimeBeforeAffectedTimer -= Time.deltaTime; // minus 1 per second
        if (TimeBeforeAffectedTimer <= 0f)
        {
            CanBeAffected = true; // Will be affected by timestop
        }

        if (CanBeAffected && timemanager.isRewinding && !IsStopped)
        {
            //transform.Rotate(0f, 0f, 0f, Space.Self);
            // Debug.Log("Time Stop");

            if (rb.velocity.magnitude >= 0f) //If Object is moving
            {
                recordedVelocity = rb.velocity.normalized; //records direction of movement
                recordedMagnitude = rb.velocity.magnitude; // records magitude of movement

                rb.velocity *= 0f; //makes the rigidbody stop moving
                rb.isKinematic = true; //not affected by forces
               // IsStopped = true; // prevents this from looping
                

            }
        }
        else 
        {
            ContinueTime();
            
        }

    }
    public void ContinueTime()
    {
        //Debug.Log("Continue Time NOW");
        rb.isKinematic = false;
        IsStopped = false;
        rb.velocity = recordedVelocity * recordedMagnitude; //Adds back the recorded velocity when time continues
       //transform.Rotate(0f, 20f, 0f, Space.Self);


    }
}
