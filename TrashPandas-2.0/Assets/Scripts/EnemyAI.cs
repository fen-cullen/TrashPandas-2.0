using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public FSMStates currentState;
    public float attackDistance = 1;
    public float chaseDistance = 5;
    public float moveSpeed = 5;
    public Animator anim;
    public float hitRate = 2;
    public float ellapsedTime = 0;
    //public GameObject deadVFX;
    //public bool isDead = false;

    public GameObject[] wanderPoints;
    Vector3 nextDestination;
    int currentDestinationIdx = 0;
    float distToPlayer;

    NavMeshAgent agent;

    public Transform enemyEyes;
    public float fieldOfView = 150f;
    //Transform deadTransform;

    //EnemyHealth enemyHealth;
    //int health;


    public enum FSMStates
    {
        Idle,
        Moving,
        Attacking,
        Dead
    }

    private void Start()
    {
        //enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();

        if (wanderPoints == null) {
            wanderPoints = GameObject.FindGameObjectsWithTag("Wanderpoint");
        }
       
        player = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();
        
        //health = enemyHealth.currHealth;
        currentState = FSMStates.Moving;
        FindNextPoint();

    }

    private void Update()
    {
        distToPlayer = Vector3.Distance(transform.position, player.transform.position);

        //health = enemyHealth.currHealth;

        switch (currentState)
        {
            case FSMStates.Moving:
                UpdateMovingState();
                break;
            case FSMStates.Attacking:
                UpdateAttackingState();
                break;
            case FSMStates.Dead:
                UpdateDeadState();
                break;
        }

        ellapsedTime += Time.deltaTime;

        /*if (health <= 0)
        {
            currentState = FSMStates.Dead;
        }*/
    }

    private void UpdateMovingState()
    {
        anim.SetInteger("AnimState", 1);

        if (distToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attacking;
        }
        else
        {
            if (distToPlayer <= chaseDistance && isPlayerInClearFOV())
            {
                print("Chasing");
                nextDestination = player.transform.position;
            }
            else
            {
                print("Patrolling");
                if (Vector3.Distance(transform.position, nextDestination) < 2)
                {
                    FindNextPoint();
                }
            }
            
            agent.stoppingDistance = 0;
            agent.speed = 3.5f;

            FaceTarget(nextDestination);
            agent.SetDestination(nextDestination);
        }
    }

    private void UpdateAttackingState()
    {
        print("attacking");
        anim.SetInteger("AnimState", 2);
        nextDestination = player.transform.position;
        FaceTarget(nextDestination);

        if (ellapsedTime > hitRate)
        {
            ellapsedTime = 0;
            if (distToPlayer <= attackDistance)
            {
                GameObject.Find("GameManager").GetComponent<GameController>().TakeDamage(1);
            }
        }

        
        if (distToPlayer > attackDistance)
        {
            currentState = FSMStates.Moving;
        }

    }

    

    private void UpdateDeadState()
    {
        print("dead");
        anim.SetInteger("AnimState", 3);
        //deadTransform = gameObject.transform;
        //isDead = true;
        Destroy(gameObject, 3);
    }

    void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIdx].transform.position;
        currentDestinationIdx = (currentDestinationIdx + 1) % wanderPoints.Length;

        agent.SetDestination(nextDestination);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5 * Time.deltaTime);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, chaseDistance);

        Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * chaseDistance);
        Vector3 leftRayPoint = Quaternion.Euler(0, fieldOfView * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -fieldOfView * 0.5f, 0) * frontRayPoint;

        Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
        Debug.DrawLine(enemyEyes.position, leftRayPoint, Color.yellow);
        Debug.DrawLine(enemyEyes.position, rightRayPoint, Color.yellow);
    }

    bool isPlayerInClearFOV() {
        Vector3 directionToPlayer = player.transform.position - enemyEyes.position;

        if (Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOfView) {
            RaycastHit hit;
            if (Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, chaseDistance)) {
                if (hit.transform.CompareTag("Player")) {
                    return true;
                }
            }
        }

        return false;
    }

    private void OnDestroy()
    {
        //Instantiate(deadVFX, deadTransform.position, deadTransform.rotation);
    }
}
