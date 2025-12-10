using UnityEngine;
using UnityEngine.AI;

public class FleeState : EnemyStateBase
{
    public FleeState(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private float fleeDistance = 5f;
    private Vector3 lastFleeDirection;
    public override void Update()
    {
        Vector3 fleeDirection = (enemyAI.transform.position - enemyAI.target.position).normalized;

        Vector3 fleeTarget = enemyAI.transform.position + fleeDirection * fleeDistance;

        NavMeshPath path = new NavMeshPath();

        // If there's a path
        if (NavMesh.CalculatePath(enemyAI.transform.position, fleeTarget, NavMesh.AllAreas, path))
        {
            lastFleeDirection = (path.corners[1] - enemyAI.transform.position).normalized;
        }
        // If there's not path
        else
        {
            Vector3 changeDirection = Vector3.Cross(Vector3.up, fleeDirection).normalized;
            fleeTarget = enemyAI.transform.position + changeDirection * fleeDistance;

            if (NavMesh.CalculatePath(enemyAI.transform.position, fleeTarget, NavMesh.AllAreas, path))
            {
                lastFleeDirection = (path.corners[1] - enemyAI.transform.position).normalized;
            }
            // If still no
            else
            {
                lastFleeDirection = fleeDirection;
            }
        }

        enemyAI.agent.SetDestination(enemyAI.transform.position + lastFleeDirection * 3f);
    }
}
