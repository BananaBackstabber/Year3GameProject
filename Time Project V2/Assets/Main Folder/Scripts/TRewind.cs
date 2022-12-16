using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRewind : MonoBehaviour
{
    public bool isRewinding = false;

    List<PointInTime> pointsInTime;

    Rigidbody rb;

    public Material timeMat;

    public float recordTime = 5f;
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            StartRewind();
        if (Input.GetKeyUp(KeyCode.Return))
            StopRewind();
    }

    private void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    
    }

    void Rewind ()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        } else
        {
            StopRewind();
        }

    } 
    
      
    void Record()
    {
        if(pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime)) 
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));

    }

    void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
        timeMat.color = Color.blue;
    }

    void StopRewind() {
        isRewinding = false;
            rb.isKinematic = false;
        timeMat.color = Color.black;
    }
}
