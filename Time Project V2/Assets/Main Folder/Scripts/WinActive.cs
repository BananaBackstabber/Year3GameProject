using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinActive : MonoBehaviour
{
    private int nextSceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;


    }

    private void OnTriggerStay(Collider other)
    {
        // If player collides with the Time Crtystal them the rope and win box spawn in at the entery point 
        // AKA The Win codition has been met
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Next_level");

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }



}
