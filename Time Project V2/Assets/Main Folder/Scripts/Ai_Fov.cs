using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai_Fov : MonoBehaviour
{
    
   //FOV SET UP (NEEDS title)
    //For Chase PLAYER
    public bool IsSeen = false;

    //The field of view
    public float distance = 10;
    public float angle = 30;
    public float height = 1.0f;
    public Color meshColor = Color.red;
    // Sensor check
    public int scanFrequency = 30;
    public LayerMask layers;
    //List of game objects for detection
    public List<GameObject> Objects = new List<GameObject>();
    //Layer mask that stop detection through walls
    public LayerMask occlusionLayers;


    Collider[] colliders = new Collider[50];
    Mesh mesh;
    int count;
    float scanInterval;
    float scanTimer;


    //AI ACTION VARIABLES (Needs title)
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
   // public float sightRange, attackRange;
    public bool playerInAttackRange;

    // time stop

    private TimeManager timemanager;

    private bool delay;
    private float delayTimer = 10;

    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        scanInterval = 1.0f / scanFrequency;
    }

    void Update()
    {
        Pursue();
        scanTimer -= Time.deltaTime;
        if (scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();

        }

        //if player is not in FOV then Patrol
        if (IsSeen == false && !playerInAttackRange && delay == false) patroling();
        //If the player is Seen then emey moves to attack range
        if (IsSeen == true && !playerInAttackRange && delay == false) ChasePlayer();
        //when in attack range, Shoot the player
        if (IsSeen == true && playerInAttackRange && delay == false) AttackPlayer();
    }

    private void Scan()
    {
        count = Physics.OverlapSphereNonAlloc(transform.position, distance, colliders, layers, QueryTriggerInteraction.Collide);

        //Objects count detection
        Objects.Clear();
        for (int i = 0; i < count; ++i)
        {

            GameObject obj = colliders[i].gameObject;
            if (IsInSight(obj))
            {
                Objects.Add(obj);

            }

        }

    }

    public bool IsInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;
        if (direction.y < 0 || direction.y > height)
        {
            return false;
        }

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if (deltaAngle > angle)
        {
            return false;
        }

        origin.y += height / 2;
        dest.y = origin.y;
        if (Physics.Linecast(origin, dest, occlusionLayers))
        {
            return false;
        }
        return true;

    }

    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        //build of the triangle cone 
        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;

        int vert = 0;




        //left side 
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = bottomRight;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;
        // Right side

        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        //caluclating the sides of each segment
        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;

        for (int i = 0; i < segments; ++i)
        {
            //Redifining the bottom left and bottom right points 

            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;

            topRight = bottomRight + Vector3.up * height;
            topLeft = bottomLeft + Vector3.up * height;

            // far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            // top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;
            // bottom 
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;
            currentAngle += deltaAngle;
        }



        //Triangles array decaled ealier

        for (int i = 0; i < numVertices; ++i)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();


        return mesh;

    }

    private void OnValidate()
    {
        mesh = CreateWedgeMesh();
        scanInterval = 1.0f / scanFrequency;
    }

    private void Pursue()
    {
        foreach (var obj in Objects)
        {


            if (obj.tag == "Player")
            {
                //Debug.Log("CHASE AND KILL");
                IsSeen = true;
                Invoke("AfterPursue", 10);
               

            }
        }
    }

    private void AfterPursue()
    {

        //If player is not in sight anymore then stop pursuing and reset to patroling
        IsSeen = false;
        Invoke("Pursue", 0);



    }
    private void OnDrawGizmos()
    {//FOV
        if (mesh)
        {

            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }
        
       
        Gizmos.DrawWireSphere(transform.position, distance);
        for (int i = 0; i < count; ++i)
        {

            Gizmos.DrawSphere(colliders[i].transform.position, 0.8f);

        }

        
        Gizmos.color = Color.green;
        foreach (var obj in Objects)
        {
            Gizmos.DrawSphere(obj.transform.position, 0.9f);
        }


    }



    void patroling()
    {


        // IF distance is less than 1 then update patrol information
        if (Vector3.Distance(transform.position, Target) < 1.5)
        {
            IterateWaypointIndex();
            patrolingWalkPoint();

        }


    }

    //AI ACTIONS (Not part of FOV Set up)
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
        if (waypointIndex == waypoints.Length)
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

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), 10f * Time.deltaTime); //Look at player

        }


    }

    void AttackPlayer()
    {

        if (timemanager.isRewinding == false && delay == false)
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
