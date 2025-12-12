using UnityEngine;

public class PatrolState :EnemyStateBase
{
    public PatrolState(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        enemyAI.currentPatrolIndex = 0;
    }

    public override void Update()
    {
        if (enemyAI.agent.remainingDistance <= 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    private void GoToNextPatrolPoint()
    {
        // Move agent
        enemyAI.agent.SetDestination(enemyAI.patrolPoints[enemyAI.currentPatrolIndex].position);

        //Set patrol point to the next patrol points
        enemyAI.currentPatrolIndex++;
        if (enemyAI.currentPatrolIndex >= enemyAI.patrolPoints.Count)
        {
            enemyAI.currentPatrolIndex = 0;
        }

    }
}
