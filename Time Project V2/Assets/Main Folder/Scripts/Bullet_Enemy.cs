using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{
    //public int playerIsAt;

    public Player player1;
    
    public float damage;

    void Start() 
    {
       player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(gameObject.tag);
        if(collision.gameObject.tag == "Player") 
        {
            playerDamaged();
            Destroy(gameObject);
        }
        else if(collision.gameObject.layer == 8)// Check the Int value of the layer 
        {
            //Invoke("bulletDeath", 0.7f);
            Destroy(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
        
    }

    void bulletDeath() 
    {
        Destroy(gameObject);
    }
    void playerDamaged() 
    {
        player1.Current_health -= damage;
        player1.HealthBar.SetPlayerHealth(player1.Current_health);
        //Debug.Log(player1.Current_health);
        
    }



}
