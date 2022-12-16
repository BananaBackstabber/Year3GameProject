using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject exp;

    public float expForce, radius; // using comma creates 2 variables



    void Start()
    {
        StartCoroutine(TimeDelay());
        //Destroy(gameObject);
    }

    void knockBack() 
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearby in colliders) 
        {
            Rigidbody rigg = nearby.GetComponent<Rigidbody>();
            if(rigg != null)
            {

                rigg.AddExplosionForce(expForce, transform.position, radius);

            }
        }
    }

    IEnumerator TimeDelay()
    {

        yield return new WaitForSeconds(1);

        GameObject _exp = Instantiate(exp, transform.position, transform.rotation); // where the explosion happens
        Destroy(_exp, 3);
        knockBack();


    }
}
