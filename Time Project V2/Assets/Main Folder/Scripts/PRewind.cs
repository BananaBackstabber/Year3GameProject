using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRewind : MonoBehaviour
{

    public TimeManager TimeManager;

    public Select_powers poweractive;

    //Records the player position to be reversed
    List<PointInTime> pointsInTime;
    //Records the players health at any point in time
    List<RecordHealth> RecordedHealth;
    //public float Phealth;

    public Player player;

    public PlayerSprintandCrouch Speed;

    public float Timespeed;
    public float Timenormal;
    public float hreverse;
    public Material timeMat;

    public float recordTime = 5f;

    private bool toggle;

    void Start()
    {
        TimeManager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        pointsInTime = new List<PointInTime>();
        RecordedHealth = new List<RecordHealth>();
        poweractive = GameObject.FindGameObjectWithTag("Power_selecter").GetComponent<Select_powers>();
        
        
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(pointsInTime);

       

        if(poweractive.isreversecurrent == true) 
        {
            //Debug.Log("RIGHTCLICKR");
            if (Input.GetMouseButtonDown(1) && TimeManager.cooldown == false && TimeManager.TimeIsSlow == false)
                toggletime();

        }
        
    }

    private void FixedUpdate()
    {
        if (TimeManager.isRewinding)
            Rewind();
        else
            Record();
            

    }

    void toggletime()
    {
        toggle = !toggle;

        if (toggle)
        {
            StartRewind();
        }
        else
        {
            StopRewind();
        }


    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            //Reverses the player current position
            Time.timeScale = Timespeed;
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            //transform.localPosition = pointInTime.position;
            pointsInTime.RemoveAt(0);

            //Reverses the player current Health
            RecordHealth recordHealth = RecordedHealth[0];
            player.Current_health = recordHealth.trackHealth;
            RecordedHealth.RemoveAt(0);
            //Debug.Log("Track Health =" + recordHealth.trackHealth);
              //Debug.Log("Track Health =" + hreverse);
        }
        else
        {
            StopRewind();
        }

    }


    void Record()
    {
        //Records the players position and rotation in time
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        //Records the players health at any point in time, so it can be reversed
        if(RecordedHealth.Count > Mathf.Round(recordTime / Time.fixedDeltaTime)) 
        {
            RecordedHealth.RemoveAt(RecordedHealth.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
        RecordedHealth.Insert(0, new RecordHealth(player.Current_health));

        hreverse -= 2f * Time.deltaTime;
       // hreverse = player.Current_health;
    }

    void StartRewind()
    {
        TimeManager.isRewinding = true;
        //rb.isKinematic = true;
        timeMat.color = Color.blue;
    }

    void StopRewind()
    {
        Time.timeScale = Timenormal;
       // Speed.move_Speed = 15;
        TimeManager.isRewinding = false;
        //rb.isKinematic = false;
        timeMat.color = Color.black;
    }
}