using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent agent;
    public Transform target; // Player

    public List<Transform> patrolPoints;
    [HideInInspector]
    public int currentPatrolIndex = 0;

    public float shootDistance = 6f;
    public float chaseDistance = 10f;
    public float returnDistance = 15f;

    [HideInInspector]
    public Vector3 startPosition;

    public CombatState currentParentState;

    public int maxHP = 3;
    public int HP = 3;
    public int fleeHP = 1;

    public GameObject hitParticlePrefab;

    public int ammoAmount = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        tmp = GetComponentInChildren<TextMeshPro>();
    }

    private void Start()
    {
        startPosition = transform.position;

        currentParentState = new CombatState(this);
        currentParentState.Enter();
    }

    private void Update()
    {
        currentParentState.Update();

        UpdateVisualStateInGame();
    }

    private TextMeshPro tmp;
    private void UpdateVisualStateInGame()
    {
        tmp.text = currentParentState.currentState.GetType().Name;
    }
 

    public GameObject projectilePrefab;
    public float shootTime = 2;
    public void EnemyShoot()
    {
        if(ammoAmount > 0)
        {
            GameObject obj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Vector3 distance = target.position - transform.position;
            Vector3 horizontalDistance = new Vector3(distance.x, 0, distance.z);
            float horizontalVelocity = horizontalDistance.magnitude / shootTime;

            float verticalVelocity = (distance.y - 0.5f * Physics.gravity.y * shootTime * shootTime) / shootTime;

            Vector3 finalVelocity = horizontalDistance.normalized * horizontalVelocity;
            finalVelocity.y = verticalVelocity;

            obj.GetComponent<Rigidbody>().linearVelocity = finalVelocity;
            ammoAmount -= 1;

            if(ammoAmount < 0)
            {
                ammoAmount = 0;
            }
        }
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
                currentParentState.ChangeState(new DeadState(this));
                return;
            }
            if(HP <= fleeHP)
            {
                currentParentState.ChangeState(new FleeState(this));
                FlickerEye();
            }
        }

    }

    private void FlickerEye()
    {
        GetComponentInChildren<MaterialFlicker>().isFlicker = true;
    }
    public bool isPlayerInChaseRange()
    {
        float distance2Player = Vector3.Distance(target.position, transform.position);
        if (distance2Player <= chaseDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isPlayerInShootRange()
    {
        float distance2Player = Vector3.Distance(target.position, transform.position);
        if (distance2Player <= shootDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isEnemyHPInDanger()
    {
        if (HP <= fleeHP) return true;
        else return false;
    }
    public bool isEnemyOutOfReturnRange()
    {
        float distance2Player = Vector3.Distance(target.position, transform.position);
        if (distance2Player > returnDistance) return true;
        else return false;

    }
    public GameObject GetNearestAmmo()
    {
        GameObject[] ammos = GameObject.FindGameObjectsWithTag("Ammo");
        GameObject nearest = null;

        float minDistance = Mathf.Infinity;

        foreach(GameObject a in ammos)
        {
            float distance = Vector3.Distance(transform.position, a.transform.position);
            if(distance <= 50f)
            {
                NavMeshHit hit;
                if(NavMesh.SamplePosition(a.transform.position,out hit,5f, NavMesh.AllAreas))
                {
                    if(distance < minDistance)
                    {
                        minDistance = distance;
                        nearest = a;
                    }
                }
            }
        }
        return nearest;
    }

    public void AddHP(int hp)
    {
        HP += hp;

        if(HP> maxHP)
        {
            HP = maxHP;
        }
    }


}

