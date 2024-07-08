using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.Image;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public PlayerMovement playerMovement;
    private float healthPoints;

    public LayerMask whatIsGround, whatIsPlayer;


    public float health;

    //sprite
    [Header("Sprite")]
    private Animator spriteAnim;
    private AngleToPlayer angleToPlayer;


    //patroling
    [Header("Patrol")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attacking
    [Header("Attacking")]
    public float timeBetweenAttacks;
    public float bulletSpeed;
    //public float bulletTime;
    public float bulletDelay;
    public float bulletDamage;
    bool alreadyAttacked;
    //public GameObject projectile;
    public Transform shootPoint;

    //states
    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, playerInLineOfSight;
    




    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    // Start is called before the first frame update
    void Start()
    {
        spriteAnim = GetComponentInChildren<Animator>();
        angleToPlayer = GetComponent<AngleToPlayer>();

        if(player == null)
        {
            player = GameObject.Find("Player").transform;
        }
        if (playerMovement == null)
        {
            playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }
       

    }

    private void Patroling()
    {
        Debug.Log("Patroling");
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //if walkpoint reached then..
        if (distanceToWalkPoint.magnitude < 1f) 
            walkPointSet = false;
    }
    
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        Debug.Log("Chasing");
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }

    private void SearchPlayer()
    {
        Debug.Log("Searching");
        agent.SetDestination(player.position);
        
    }
    private void AttackPlayer()
    {
        Debug.Log("Attacking");

        //check if enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        if (!alreadyAttacked)
        {

            //Raycast bullets
            StartCoroutine(RayBullet());

            //Shoot projectile clones, and delete them
            //attack
            //Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);

            //destroy clone in 2sec
            //Destroy(rb.gameObject, bulletTime);

            //check attack
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    IEnumerator RayBullet()
    {
        Vector3 startPosition = shootPoint.position;
        Vector3 shootDirection = shootPoint.forward;

        yield return new WaitForSeconds(bulletDelay);

        RaycastHit hit;
        if (Physics.Raycast(startPosition,shootDirection,out hit, attackRange*2))
        {
            if(hit.collider.gameObject == player.gameObject)
            {
                //Debug.DrawRay(transform.position, transform.forward, Color.cyan);
                playerMovement.healthPoints -= bulletDamage;
                //Debug.Log(playerMovement.healthPoints);
            } else
            {
                //Debug.Log(hit.collider.gameObject);
            }
        }
            
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Invoke(nameof(DestroyEnemy), 0);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }


    // Update is called once per frame
    private void Update()
    {
        //sprite
        //Debug.Log(angleToPlayer.lastIndex);
        spriteAnim.SetFloat("spriteRot", angleToPlayer.lastIndex);

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            // Parameters for the cone raycast
            float coneAngle = 45f; // Angle of the cone in degrees
            int numRays = 9; // Number of rays within the cone
            float rayDistance = 100f; // Maximum distance of the rays

        // Reset playerInLineOfSight
        playerInLineOfSight = false;

        // Loop to cast multiple rays within the cone angle
        for (int i = 0; i < numRays; i++)
        {
            // Calculate the angle for this ray
            float angle = -coneAngle / 2 + coneAngle * (i / (float)(numRays - 1));
            // Calculate the direction for this ray
            Vector3 rayDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(transform.position, rayDirection, out hit, rayDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    //Debug.Log("Player detected by raycast!");
                    Debug.DrawRay(transform.position, rayDirection * rayDistance, Color.red);
                    playerInLineOfSight = true;
                    
                   
                }
                
                // Debug.Log(hit.collider);
            }
            else
            {
                Debug.DrawRay(transform.position, rayDirection * rayDistance, Color.green);
            }
        }

        if (!playerInSightRange && !playerInAttackRange && !playerInSightRange) Patroling(); //search if doesnt know where player is
        if (playerInSightRange && !playerInAttackRange && !playerInLineOfSight) SearchPlayer(); //search if player is in range but not in LOS
        if (playerInSightRange && !playerInAttackRange && playerInLineOfSight) ChasePlayer(); //chase player if LOS but no range
        if (playerInSightRange && playerInAttackRange && playerInLineOfSight) AttackPlayer(); // attack if LOS and range
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
