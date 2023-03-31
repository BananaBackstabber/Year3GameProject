using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top_Down_Controls : MonoBehaviour
{

    private float m_fov = 90f;

    public float max;
    public float min;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographicSize = m_fov;
    }

    // Update is called once per frame
    void Update()
    {

        //INPUT to increase and decrease the cameras field of view
        if (Input.GetKey("w")) 
        {
            m_fov -= 1;
            Camera.main.orthographicSize = m_fov;
        }
        if (Input.GetKey("s")) 
        {  
            m_fov += 1;
            Camera.main.orthographicSize = m_fov;
        }

        // If FOV goes over min or max values it is equal to those min or max values
        if (m_fov <= min)
        {
            m_fov = min;
        }
        else if (m_fov >= max)
        {
            m_fov = max;

        }
    }
}
