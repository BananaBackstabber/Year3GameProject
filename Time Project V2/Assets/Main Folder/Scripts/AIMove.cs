using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{
    //Attack Code start
    //bullet
    public GameObject bullet;
    // public GameObject currentBullet;
    //bullet force
    public float shootForce, upwardForce;
    
    //Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    //public int magazineSize, bulletsPerTap;
    //public bool allowButtonHold;

    //int bulletsLeft, bulletShot;

    //bools
    bool readyToShoot;

    //Refrences
    public Camera fpsCam;
    public Transform attackPoint;

    //Attack Code end 

    //scripts
    private TimeBody timeIsStopped;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking 
    public float timeBetweenAttacks;
    public float DefualtAttackTime;
    bool alreadyAttacked;
    public float TimeSlow;
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // time stop

    private TimeManager timemanager;

    public float Speed;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }
     void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag("Player");
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
    }
    // Update is called once per frame
    void Update()
    {

        Debug.Log("Agent Velocity" + agent.acceleration);
        Debug.Log("Agent Speed" + agent.speed);
        // if Time Is not stopped then
        if (!timemanager.TimeIsStopped)
        {
            //Debug.Log("TimeOff");
            timeBetweenAttacks = DefualtAttackTime;
            agent.speed = Speed;

            agent.SetDestination(player.transform.position);  //go to player

           if (agent.remainingDistance <= agent.stoppingDistance)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), 10f * Time.deltaTime); //Look at player
                
            }
        }
        // if time is stopped then
        if (timemanager.TimeIsStopped)
        {
            //Debug.Log("TimeOn");
            agent.speed *= TimeSlow;
            agent.velocity *= TimeSlow;
            agent.acceleration *= TimeSlow; // stop moving
            //agent.updateRotation = false;
            //Debug.Log("STOPPED");
            timeBetweenAttacks = 3;
        }
        
        
            
            //Normal Movement
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        

       

    }

    void patroling() 
    {
        //Debug.Log("WALKING AROUND");
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) 
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

    }

    void SearchWalkPoint() 
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    void ChasePlayer() 
    {
        
            agent.SetDestination(player.position);
             //Debug.Log("CHASING PLAYER");
           // Debug.Log("Off Sight =" + sightRange);
       

    }

    void AttackPlayer() 
    {

        agent.SetDestination(transform.position);

        transform.LookAt(player);
    

        if(!alreadyAttacked)
        {
            //attack code

            shoot();

            //Debug.Log("PlayerIsAttacked");

            alreadyAttacked = true;
            Invoke(nameof(resetAttack), timeBetweenAttacks);

        }
    
    }

    void resetAttack() 
    {

        alreadyAttacked = false;

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
        



 
            


    }







}
