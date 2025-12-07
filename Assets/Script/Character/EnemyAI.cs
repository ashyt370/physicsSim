using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Patrolling,
    ChasingPlayer,
    Fleeing,
    Returning,
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
                if(distance2Player > returnDistance)
                {
                    currentState = EnemyState.Returning;
                }
                Vector3 fleePosition = new Vector3(-target.position.x, 0, -target.position.z);
                agent.SetDestination(fleePosition);
                break;
            default:
                Debug.LogError("no state");
                break;
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

    private void OnCollisionEnter(Collision collision)
    {
        // If its bullet
        if(collision.gameObject.CompareTag("Bullet"))
        {
            HP--;
            Debug.Log("Enemy got hit");
            if(HP <= 0)
            {
                GetComponent<Collider>().enabled = false;
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

