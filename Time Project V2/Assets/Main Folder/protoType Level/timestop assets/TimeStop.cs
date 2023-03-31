using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    public float TimeBeforeAffected; //The time after the object spawns until it will be affected by the timestop(for projectiles etc)
    public TimeManager timemanager;
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

        //Debug.Log("Time = " + timemanager.TimeIsSlow);
        //Debug.Log("canbeAffected =" + CanBeAffected);
        TimeBeforeAffectedTimer -= Time.deltaTime; // minus 1 per second
        if (TimeBeforeAffectedTimer <= 0f)
        {
            CanBeAffected = true; // Will be affected by timestop
        }

        if (CanBeAffected && timemanager.TimeIsSlow && !IsStopped)
        {
            if (rb.velocity.magnitude >= 0f) //If Object is moving
            {
                recordedVelocity = rb.velocity.normalized; //records direction of movement
                recordedMagnitude = rb.velocity.magnitude; // records magitude of movement

                rb.velocity *= 0.1f; //makes the rigidbody stop moving
                //rb.isKinematic = false; //not affected by forces
                IsStopped = true; // prevents this from looping
                
            }
        }

    }
    public void ContinueTime()
    {
        // So when slow time is no longer active then objects go back to the velocity and magnitude before timeslow
        if(IsStopped == true) 
        {
            rb.velocity = recordedVelocity * recordedMagnitude;
        }
        //This needs to be read last or there will be problems with bullets going bac to normal
        IsStopped = false;



    }
}
