using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRewind : MonoBehaviour
{

    public TimeManager TimeManager;

    public Select_powers poweractive;

    List<PointInTime> pointsInTime;

    public PlayerSprintandCrouch Speed;

    public float Timespeed;
    public float Timenormal;

    public Material timeMat;

    public float recordTime = 5f;

    private bool toggle;

    void Start()
    {
        TimeManager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        pointsInTime = new List<PointInTime>();
        poweractive = GameObject.FindGameObjectWithTag("Power_selecter").GetComponent<Select_powers>();
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(poweractive.reverseIsClick == true) 
        {
            if (Input.GetMouseButtonDown (1) && TimeManager.cooldown == false && TimeManager.TimeIsSlow == false)
                toggletime();
            Debug.Log("Rclick = Reverse Time");
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
            Time.timeScale = Timespeed;
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            //transform.localPosition = pointInTime.position;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }

    }


    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));

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