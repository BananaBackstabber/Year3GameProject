using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickUp : MonoBehaviour
{
    public TimeManager timemanager;
    public float TimeBack;


    private void Start()
    {
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            timemanager.TimeGauge += TimeBack;
            Debug.Log("Pick up Power");
            Destroy(gameObject);

        

        }
    }
}
