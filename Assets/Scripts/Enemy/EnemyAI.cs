using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //Reference to NavMesh
    NavMeshAgent nm;
    //How to find the player using its transform
    public Transform target;

    public float distanceThreshold = 30f;
    public int damage = 20;
    public enum AIState { idle, chasing, attack };

    public AIState aiState = AIState.idle;

    public Animator animator;
    public float attackThreshold = 1f;

    // Start is called before the first frame update
    void Start()
    {
        nm = GetComponent<NavMeshAgent>();
        StartCoroutine(Think());
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    IEnumerator Think()
    {
        while (true)
        {
            switch (aiState)
            {
                //When player transform dist < distance threshold set AIState to chasing, set chasing bool in animation to true and have the enemy move towards the player transform
                case AIState.idle:
                    float dist = Vector3.Distance(target.position, transform.position);
                    if (dist < distanceThreshold)
                    {
                        aiState = AIState.chasing;
                        animator.SetBool("Chasing", true);
                    }
                    nm.SetDestination(transform.position);
                    break;
                // when player is not within distanceThreshold play Idle animation and do not move towards player
                case AIState.chasing:
                    dist = Vector3.Distance(target.position, transform.position);
                    if (dist > distanceThreshold)
                    {
                        aiState = AIState.idle;
                        animator.SetBool("Chasing", false);
                    }
                    if (dist < attackThreshold)
                    {
                        //Enter attack state
                        aiState = AIState.attack;
                        animator.SetBool("Attacking", true);
                    }
                    nm.SetDestination(target.position);
                    break;
                case AIState.attack:
                    //Do attack stuff
                    Debug.Log("Attack!");
                    nm.SetDestination(transform.position);
                    dist = Vector3.Distance(target.position, transform.position);
                    if (dist > attackThreshold)
                    {
                        aiState = AIState.chasing;
                        Attack();
                    }
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    void Attack()
    {
       
    }
}