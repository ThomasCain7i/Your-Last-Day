using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent; // Reference to the NavMeshAgent component
    public Transform player; // Reference to the player's transform

    public LayerMask whatIsGround, whatIsPlayer; // Layer masks for ground and player

    [Header("Stats")]
    public float speed; // Movement speed of the enemy
    public float damage; // Damage caused by the enemy

    // Patrolling
    [Header("Patrolling")]
    public Transform[] patrolPoints; // Array of patrol points
    private int currentPatrolIndex; // Index of the current patrol point

    // Attacking
    [Header("Attacking")]
    public float timeBetweenAttacks; // Time between attacks
    [SerializeField]
    private bool alreadyAttacked; // Flag indicating if the enemy has already attacked
    public GameObject hitBox; // Hit box game object for attacking
    public float distanceFromPlayer; // Distance from the player
    private float lastAttackTime; // Time of the last attack

    // States
    [Header("States")]
    public float sightRange; // Range for detecting the player
    public float attackRange; // Range for attacking the player
    public bool playerInSightRange, playerInAttackRange, canSee; // Flags indicating if the player is within sight range and attack range

    [Header("Animation")]
    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform; // Find and assign the player's transform
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component of the enemy
        speed = Random.Range(1, 5);
        agent.speed = speed; // Set the movement speed of the enemy
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); // Check if the player is within sight range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); // Check if the player is within attack range

        if (!playerInSightRange && !playerInAttackRange)
            Patroling(); // If the player is not in sight or attack range, patrol
        else if (playerInSightRange && !playerInAttackRange)
            ChasePlayer(); // If the player is in sight range but not attack range, chase the player
        else if (playerInAttackRange && playerInSightRange)
            AttackPlayer(); // If the player is in attack range and sight range, attack the player

        // Perform raycast
        RaycastHit hit;
        Vector3 direction = player.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            // Check if the raycast hits the player
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Collectable"))
            {
                // Player is hit, do something
                canSee = true;
            }
            else
            {
                // There is an obstacle between the player and sniper, do something else
                canSee = false;
            }
        }
    }

    private void Patroling()
    {
        animator.SetBool("Chasing", false);
        animator.SetBool("Attacking", false);

        if (patrolPoints.Length == 0)
        {
            Debug.LogWarning("No patrol points assigned!"); // Log a warning if no patrol points are assigned
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Reached the current patrol point, move to the next one
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; // Increment the patrol point index
            agent.SetDestination(patrolPoints[currentPatrolIndex].position); // Set the destination to the next patrol point
        }
    }

    private void ChasePlayer()
    {
        animator.SetBool("Chasing", true);
        animator.SetBool("Attacking", false);

        Vector3 targetPosition = player.position - (transform.forward * distanceFromPlayer); // Calculate the target position to chase the player
        agent.SetDestination(targetPosition); // Set the destination to the target position
    }

    private void AttackPlayer()
    {
        animator.SetBool("Chasing", false);
        animator.SetBool("Attacking", true);

        if (canSee)
        {
            Vector3 targetPosition = player.position - (transform.forward * distanceFromPlayer); // Calculate the target position to attack the player
            agent.SetDestination(targetPosition); // Set the destination to the target position

            if (!alreadyAttacked && Time.time - lastAttackTime >= timeBetweenAttacks)
            {
                // Attack code here
                hitBox.SetActive(true); // Activate the hit box for attacking
                                        // End of attack code

                alreadyAttacked = true; // Set alreadyAttacked flag to true
                lastAttackTime = Time.time; // Update the last attack time

                Invoke(nameof(ResetAttack), timeBetweenAttacks); // Call ResetAttack method after the specified time
            }
        }
    }

    private void ResetAttack()
    {
        hitBox.SetActive(false); // Deactivate the hit box
        alreadyAttacked = false; // Reset the alreadyAttacked flag
        lastAttackTime = Time.time; // Update the last attack time
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Draw a wire sphere representing the attack range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange); // Draw a wire sphere representing the sight range
    }
}