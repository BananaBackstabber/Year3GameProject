using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class projectiles : MonoBehaviour
{
    //Rewind script 
    public TimeManager Time;

    //bullet
    public GameObject bullet;
   // public GameObject currentBullet;
    //bullet force
    public float shootForce, upwardForce;

    //Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    int bulletsLeft, bulletShot;

    //bools
    bool shooting, readyToShoot, reloading;

    //Refrences
    public Camera fpsCam;
    public Transform attackPoint;

    //Graphics 
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    //bug fixing
    public bool allowInvoke = true;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;


    }
    private void Update()
    {
        MyInput();

        //Set ammo display, if it exists
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText("Bullets Left = " + bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
    }

    void MyInput() 
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);
    
      

      //reloading
      if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        //Reload Automatically when trying to shoot, no ammo
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        //Shooting if time is rewind then you can't shoot
       if (readyToShoot && shooting && !reloading && bulletsLeft > 0  && Time.isRewinding == false)
        { 
            bulletShot = 0;

            shoot();
        }


    }

    void shoot() 
    {
        readyToShoot = false;

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Ray through the middle of the screen
        RaycastHit hit;

        //Checks to hit something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); // a point far away from self

        // calculate direction from target
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        // calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        //Create the Projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, attackPoint.rotation);

        currentBullet.GetComponent<Rigidbody>().velocity = currentBullet.transform.forward * shootForce;
        //Roatate Bullet in shoot direction
        //currentBullet.transform.forward = directionWithSpread.normalized;

        //MuzzleFlash, if there is one
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        //Add force to bullet
        //currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        //currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
        


        bulletsLeft--;// count down bullets left 
        bulletShot++; // count up bullets shot

        //Invoke reset shot when invoke is already in place
        if (allowInvoke) 
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

         //if more than one bulletPerTap make sure to repeat shoot function 
        if ((bulletShot < bulletsPerTap) && (bulletsLeft > 0))
            Invoke("Shoot", timeBetweenShots);

        //Play sound of Gun Shot when player shoots
        FindObjectOfType<audiomanager>().Play("Player gun shot");


    }

    void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    void Reload() 
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    void ReloadFinished() 
    {

        bulletsLeft = magazineSize;
        reloading = false;
    }
}
