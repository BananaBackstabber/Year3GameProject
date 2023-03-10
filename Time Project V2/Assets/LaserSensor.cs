using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSensor : MonoBehaviour
{
    public LaserDeath laser;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("SENSORTRIPPED");
        laser.OnSensor();
    }


}
