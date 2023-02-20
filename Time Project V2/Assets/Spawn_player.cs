using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_player : MonoBehaviour, IClick
{
    public GameObject Spawn_Object;
    public GameObject Spawn_Location;
    public void onClickAction()
    {

        Debug.Log("WORKED");
        Instantiate(Spawn_Object, Spawn_Location.transform.position, transform.rotation);
    }
  
}
