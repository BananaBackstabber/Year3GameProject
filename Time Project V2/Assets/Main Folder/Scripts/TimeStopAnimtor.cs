using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopAnimtor : MonoBehaviour
{
    public float TimeBeforeAffected; //The time after the object spawns until it will be affected by the timestop(for projectiles etc)
    private TimeManager timemanager;

    private float TimeBeforeAffectedTimer;
    private bool CanBeAffected;
    public bool IsStopped;
    public Animator slowTime;
    public float SlowSpeed;
    public float NormalSpeed;



    // Start is called before the first frame update
    void Start()
    {
        slowTime.speed = NormalSpeed;
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        TimeBeforeAffectedTimer = TimeBeforeAffected;
    }

    // Update is called once per frame
    void Update()
    {
        TimeBeforeAffectedTimer -= Time.deltaTime; // minus 1 per second
        if (TimeBeforeAffectedTimer <= 0f)
        {
            CanBeAffected = true; // Will be affected by timestop
        }

        if (CanBeAffected && timemanager.TimeIsSlow && !IsStopped)
        {

            slowTime.speed = SlowSpeed;
           
        }
        else 
        {

            slowTime.speed = NormalSpeed;
        }
    }
    public void ContinueTime()
    {
        Debug.Log("Slow SPEED");
       
        IsStopped = false;

        slowTime.speed = NormalSpeed;

    }
}
