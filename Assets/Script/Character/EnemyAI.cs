using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Patrolling,
    ChasingPlayer,
    Fleeing,
    Returning,
    Dead,
}

public class EnemyAI : MonoBehaviour
{

    private NavMeshAgent agent;
    public Transform target; // Player

    public List<Transform> patrolPoints;
    private int currentPatrolIndex = 0;

    public float chaseDistance = 10f;
    public float returnDistance = 15f;

    private Vector3 startPosition;

    public EnemyState currentState = EnemyState.Patrolling;

    public int HP = 3;
    public int fleeHP = 1;

    public GameObject hitParticlePrefab;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        startPosition = transform.position;

        // the default state is patrolling
        GoToNextPatrolPoint();
    }

    private void Update()
    {
        // determine factor: the distance between player
        float distance2Player = Vector3.Distance(target.position, transform.position);

        switch(currentState)
        {
            case EnemyState.Patrolling:
                // If player is in chase range, set state to chasing player
                if(distance2Player <= chaseDistance)
                {
                    float rand = Random.value; 
                    if (rand < 0.8f)
                        currentState = EnemyState.ChasingPlayer;
                    else
                        Debug.Log("Enemy decided not to chase player");
                }
                // If the player reach the target patrol point
                if(agent.remainingDistance <= 0.5f)
                {
                    GoToNextPatrolPoint();
                }
                break;
            case EnemyState.ChasingPlayer:
                // If player is too far away, return the enemy
                if(distance2Player > returnDistance)
                {
                    currentState = EnemyState.Returning;
                }

                agent.SetDestination(target.position);

                break;
            case EnemyState.Returning:
                // If the player is back in chase distance
                if(distance2Player <= chaseDistance)
                {
                    currentState = EnemyState.ChasingPlayer;
                }
                if(agent.remainingDistance < 0.5)
                {
                    currentState = EnemyState.Patrolling;
                    GoToNextPatrolPoint();
                }
                agent.SetDestination(startPosition);
                break;

            case EnemyState.Fleeing:
                FleeFromPlayer();

                break;

            case EnemyState.Dead:
                agent.isStopped = true;    
                break;
            default:
                Debug.LogError("no state");
                break;
        }

    }

    private float fleeDistance = 5f;
    private Vector3 lastFleeDirection;
    private void FleeFromPlayer()
    {
        Vector3 fleeDirection = (transform.position - target.position).normalized;

        Vector3 fleeTarget = transform.position + fleeDirection * fleeDistance;

        NavMeshPath path = new NavMeshPath();

        // If there's a path
        if(NavMesh.CalculatePath(transform.position,fleeTarget,NavMesh.AllAreas,path))
        {
            lastFleeDirection = (path.corners[1] - transform.position).normalized;
        }
        // If there's not path
        else
        {
            Vector3 changeDirection = Vector3.Cross(Vector3.up, fleeDirection).normalized;
            fleeTarget = transform.position + changeDirection * fleeDistance;

            if(NavMesh.CalculatePath(transform.position, fleeTarget, NavMesh.AllAreas, path))
            {
                lastFleeDirection = (path.corners[1] - transform.position).normalized;
            }
            // If still no
            else
            {
                lastFleeDirection = fleeDirection;
            }
        }

        agent.SetDestination(transform.position + lastFleeDirection * 3f);

        if(Vector3.Distance(target.position,transform.position) > returnDistance)
        {
            currentState = EnemyState.Returning;
        }

    }

    private void GoToNextPatrolPoint()
    {
        // Move agent
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);

        //Set patrol point to the next patrol points
        currentPatrolIndex++;
        if(currentPatrolIndex >= patrolPoints.Count)
        {
            currentPatrolIndex = 0;
        }

    }


    public GameObject projectilePrefab;
    public float shootTime = 2;
    private void EnemyShoot()
    {
        GameObject obj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Vector3 distance = target.position - transform.position;
        Vector3 horizontalDistance = new Vector3(distance.x, 0, distance.z);
        float horizontalVelocity = horizontalDistance.magnitude / shootTime;

        float verticalVelocity = (distance.y - 0.5f * Physics.gravity.y * shootTime * shootTime) / shootTime;

        Vector3 finalVelocity = horizontalDistance.normalized * horizontalVelocity;
        finalVelocity.y = verticalVelocity;

        obj.GetComponent<Rigidbody>().linearVelocity = finalVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If its bullet
        if(collision.gameObject.CompareTag("Bullet"))
        {
            // Prticle Effect
            Instantiate(hitParticlePrefab, collision.contacts[0].point, Quaternion.identity);

            HP--;        
            if(HP <= 0)
            {
                //GetComponent<Collider>().enabled = false;
                currentState = EnemyState.Dead;

                return;
            }
            if(HP <= fleeHP)
            {
                currentState = EnemyState.Fleeing;
                FlickerEye();
            }


        }

        // If its player
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.instance.ShowLoseScreen();
        }
    }

    private void FlickerEye()
    {
        GetComponentInChildren<MaterialFlicker>().isFlicker = true;
    }
}

