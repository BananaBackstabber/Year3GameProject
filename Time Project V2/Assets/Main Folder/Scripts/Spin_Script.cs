using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin_Script : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 150f * Time.deltaTime, 0f, Space.Self);
    }
}
