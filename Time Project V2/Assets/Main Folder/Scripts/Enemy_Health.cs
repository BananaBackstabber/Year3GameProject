using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{

    public int Health;

    public GameObject timePickUp;

    public int damagefrombullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0 )
        {
            Destroy(gameObject);
            Instantiate(timePickUp, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Hit_01");

        if(collision.gameObject.name == "Cube Bullet(Clone)")
        {
            //Debug.Log("SHOT" + Health);
            Health -= damagefrombullet;
        }
    }
}
