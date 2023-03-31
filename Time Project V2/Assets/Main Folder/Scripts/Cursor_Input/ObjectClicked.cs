using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicked : MonoBehaviour, IClick
{
    public Camera Topcam;
    // Update is called once per frame

    public void onClickAction() 
    {
    
    }
    private void Start()
    {
         Cursor.lockState = CursorLockMode.Confined;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Topcam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000.0f))// Draw a raycast when player presses left mouse click
            {
                if (hit.transform != null) // if the raycast hits something then...
                {
                   //perform the Onclick action(AKA spawn player at entry point ect...)
                    IClick click = hit.collider.gameObject.GetComponent<IClick>();
                    if (click != null) click.onClickAction();
                    
                    
                }
            }
        }
    }
}
