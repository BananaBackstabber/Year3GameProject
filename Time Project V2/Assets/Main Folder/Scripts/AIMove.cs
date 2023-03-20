using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{
    //FOV Script
    public Ai_Fov fov;

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
    private TimeBody TimeIsSlow;

    public NavMeshAgent agent;

    public Transform player;


    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //PATROL V2
    public Vector3 PatrolPoint;
    public Transform[] waypoints;
    int waypointIndex;
    Vector3 Target;

    //Attacking 
    public float timeBetweenAttacks;
    public float DefualtAttackTime;
    bool alreadyAttacked;
    public float TimeSlow;
    public float TimeStop;
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // time stop

    private TimeManager timemanager;

    private bool delay;
    private float delayTimer = 10;

    public float Speed;
    public float chaseSpeed;

    private void Awake()
    {
       // player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }
     void Start()
    {
        //Refrence to NAvmesh agent so the AI can navigate the navmesh
        agent = GetComponent<NavMeshAgent>();
        //patrolingWalkPoint();
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        agent.speed = Speed;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else 
        {
            player = null;
        }

        //Debug.Log("Agent Velocity" + agent.velocity);
        //Debug.Log("Agent acceleration" + agent.acceleration);



        // if Time Is not stopped then
        if (!timemanager.TimeIsSlow && !timemanager.isRewinding)
        {
            //Debug.Log("TimeOff");
            timeBetweenAttacks = DefualtAttackTime;
            agent.speed = Speed;
            //agent.velocity = agent.velocity;
            //agent.acceleration = agent.acceleration;
           // agent.updateRotation = true;

        }

        // if time is slow then
        if (timemanager.TimeIsSlow)
        {
            
            agent.speed = TimeSlow;
            //agent.velocity =  agent.velocity * TimeSlow;
            //agent.acceleration = agent.acceleration * TimeSlow; // stop moving
            //Debug.Log("STOPPED");
            timeBetweenAttacks = 3;
        }

        // if time is reversed then
        if(timemanager.isRewinding)
        {
           
            //Slows speed, rotation and increase timeBetweenAttacks so that can't shoot during the time the player reverses time
            agent.speed = TimeStop;
            agent.updateRotation = false;
            timeBetweenAttacks = 200;
            delay = true;
            //agent.velocity *= TimeStop;
            // agent.acceleration = TimeStop; // stop moving


        }
        else 
        {
            Checkdelay();
            agent.updateRotation = true;
        }
        
        
            
            //AI Movement
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //if player is not in FOV then Patrol
        if (fov.IsSeen == false && !playerInAttackRange && delay == false) patroling();
        //If the player is Seen then emey moves to attack range
        if (fov.IsSeen == true && !playerInAttackRange && delay == false) ChasePlayer();
        //when in attack range, Shoot the player
        if (fov.IsSeen == true && playerInAttackRange && delay == false) AttackPlayer();




    }
    void patroling() 
    {

        patrolingWalkPoint();
        // IF distance is less than 1 then update patrol information
        if (Vector3.Distance(transform.position, Target) < 1.5) 
        {
            Debug.Log("WAlk");
            IterateWaypointIndex();
            patrolingWalkPoint();

        }


    }
    void Randompatroling() 
    {
       // Debug.Log("WALKING AROUND");
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
        Debug.Log(walkPoint);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    void patrolingWalkPoint() 
    {
        Target = waypoints[waypointIndex].position;
        agent.SetDestination(Target);
    }

    void IterateWaypointIndex() 
    {
        //Increase waypoint index by 1
        waypointIndex++;
        // Resets waypoints back to zero
        if(waypointIndex == waypoints.Length) 
        {
            waypointIndex = 0;
        }
    
    }

    void ChasePlayer() 
    {
        
            agent.SetDestination(player.position);
          // Debug.Log("CHASING PLAYER");
         //Debug.Log("Off Sight =" + sightRange);
        // Debug.Log(player.position);

        agent.SetDestination(player.transform.position);  //go to player

       // agent.speed = chaseSpeed;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), 10f * Time.deltaTime); //Look at player

        }


    }

    void AttackPlayer() 
    {

        if(timemanager.isRewinding == false && delay == false) 
        {
           // Debug.Log("DONT LOOK =" + timemanager.isRewinding);
            agent.SetDestination(transform.position);
            transform.LookAt(player);
           // Debug.Log("Player is looked at");

            if (!alreadyAttacked)
            {
                //attack code

                //Debug.Log("player is shot");

                shoot();

                alreadyAttacked = true;
                Invoke(nameof(resetAttack), timeBetweenAttacks);

            }
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

        //play sound of enemy gun shot
        FindObjectOfType<audiomanager>().Play("Enemy Rifle shot");


    }

    void Checkdelay() 
    {
      
        if (timemanager.isRewinding == false && delay == true)
        {
            
            Invoke("playdelay", 0.8f);
        }
    
    }
    void playdelay() 
    {
        //Debug.Log("Delay Played");
        delay = false;
    }

}
