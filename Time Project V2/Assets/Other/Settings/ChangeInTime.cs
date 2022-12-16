using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;



public class ChangeInTime : MonoBehaviour
{
    public Material timeMat;

    private void Start()
    {
        timeMat.color = Color.black;
        //timeMat.Intensity = 1f;
    }
}
