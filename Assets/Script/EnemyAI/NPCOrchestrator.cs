using System.Collections.Generic;
using UnityEngine;

public class NPCOrchestrator : MonoBehaviour
{
    public List<EnemyAI> enemies;
    public Transform player;


    private void Update()
    {
        ManageEnemies();
    }

    private void ManageEnemies()
    {

        bool someoneIsAttacking = false;
        bool someoneIsFleeing = false;

        // Check status
        foreach(EnemyAI e in enemies)
        {
            if(e.currentParentState.currentState is ShootState || e.currentParentState.currentState is ChasePlayerState)
            {
                someoneIsAttacking = true;
            }
            if(e.currentParentState.currentState is FleeState)
            {
                someoneIsFleeing = true;
            }
        }

        foreach(EnemyAI e in enemies)
        {
            // If there's one enemy attacking player, set the other enemies to patrol
            e.currentParentState.ForceChangeState(null);
            if(someoneIsAttacking && e.currentParentState.currentState is not ShootState && e.currentParentState.currentState is not ChasePlayerState 
                && e.currentParentState.currentState is not FleeState)
            {
                e.currentParentState.ForceChangeState(new PatrolState(e));
                Debug.Log("Direct " + e.name + " to patrol cuz there's someone attacking");
            }
            // If there's one enemy fleeing from player (low hp), set one other enemy to come here and attack
            if (someoneIsFleeing && e.currentParentState.currentState is PatrolState)
            {
                e.currentParentState.ForceChangeState(new ChasePlayerState(e));
                Debug.Log("Direct " + e.name + " to help the fleeing partner");
                break;
            }
        }


        
        

        
    }
}
