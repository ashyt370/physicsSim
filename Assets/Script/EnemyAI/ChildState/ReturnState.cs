using UnityEngine;

public class ReturnState : EnemyStateBase
{
    public ReturnState(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter()
    {
        enemyAI.agent.SetDestination(enemyAI.startPosition);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        
    }
}
