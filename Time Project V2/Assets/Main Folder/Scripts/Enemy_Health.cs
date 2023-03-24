using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Health : MonoBehaviour
{

    public int Health;

    public GameObject timePickUp;

    public int damagefrombullet;

    public GameObject movement;

    public Animator animator;

    private int spawncont = 0;

    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0 )
        {
            // This code diables the AI components of this enemy whne it dies
            animator.SetBool("isDead", true);
            gameObject.GetComponent<AIMove>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
           

            if (spawncont == 0)// spawns recharge power up once
            {
                Instantiate(timePickUp, transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
                spawncont += 1;
            }

            Invoke("Death", 20);

        }
    }

    void Death() 
    {
        //gameObject.GetComponent<Rigidbody>().useGravity = true;
        Invoke("Death2", 4);
    }

    void Death2() 
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Hit_01");

        if(collision.gameObject.tag == "Bullet")
        {
            //Debug.Log("SHOT" + Health);
            Health -= damagefrombullet;
            FindObjectOfType<audiomanager>().Play("Player gun Hit");
        }
    }
}
