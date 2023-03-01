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
            //Debug.Log("CONTACT" + collision.gameObject+ "Health =" + player.Current_health);
            //playerIsAt -= 1;
        }
        Destroy(gameObject);
        //Debug.Log("Dead");




    }

    void playerDamaged() 
    {
        player1.Current_health -= damage;
        player1.HealthBar.SetPlayerHealth(player1.Current_health);
        Debug.Log(player1.Current_health);
        
    }



}
