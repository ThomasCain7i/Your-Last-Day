using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // References
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer;
    public PlayerHealth playerHealth;

    // Stats
    public float speed;
    public float speedSaved;
    public int damage;

    // Attack
    public float attackRange;
    public float stopRange;
    public float timeBetweenAttacks;
    private float lastAttackTime;
    private bool isAttacking;

    // Animation
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        speed = Random.Range(1, 5);
        speedSaved = speed;
        agent.speed = speed;
        playerHealth = FindObjectOfType<PlayerHealth>();
    }



    private void Update()
    {
        // Always chase the player
        ChasePlayer();

        // Check for attack range
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            AttackPlayer();
        }
        else if (isAttacking)
        {
            // Player moved out of attack range, reset to chasing mode
            animator.SetBool("Attacking", false);
            isAttacking = false;
        }

        if (Vector3.Distance(transform.position, player.position) <= stopRange)
        {
            agent.speed = 0;
        }
        else
        {
            agent.speed = speedSaved;
        }
    }

    private void ChasePlayer()
    {
        // Set the destination to the player's position
        agent.SetDestination(player.position);

        // Play running animation
        animator.SetBool("Chasing", true);
        animator.SetBool("Attacking", false);
    }

    private void AttackPlayer()
    {
        // Play attack animation
        animator.SetBool("Chasing", false);
        animator.SetBool("Attacking", true);

        if (Time.time - lastAttackTime >= timeBetweenAttacks)
        {

            if (playerHealth != null)
            {
                // Call the TakeDamage method on the PlayerController
                playerHealth.TakeDamage(damage);
            }

            lastAttackTime = Time.time;
            isAttacking = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw Gizmos for attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
