using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBomb : MonoBehaviour
{
    public GameObject spawnB;

    public GameObject Bomb;

   
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        spawn();
        Debug.Log("Working spawn");
    }
    
    void spawn() 
    {
        Instantiate(Bomb, spawnB.transform.position, Quaternion.identity);
        Debug.Log("Working bomb");
    }
}
