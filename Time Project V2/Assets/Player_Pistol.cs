using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Pistol : MonoBehaviour
{
    public Animator animator;

    

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Root() 
    {
        animator.applyRootMotion = true;
    }
   public void NoRoot() 
    {
        animator.applyRootMotion = false;
    
    }
}
