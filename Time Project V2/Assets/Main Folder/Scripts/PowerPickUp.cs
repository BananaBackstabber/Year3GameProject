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
            FindObjectOfType<audiomanager>().Play("Recharge Power Pick Up");
            timemanager.TimeGauge += TimeBack;
            Destroy(gameObject);
        }
    }
}
