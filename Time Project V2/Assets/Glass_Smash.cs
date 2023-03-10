using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass_Smash : MonoBehaviour
{
    public GameObject brokenGlass;

  

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("WORKING");
        Instantiate(brokenGlass, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
