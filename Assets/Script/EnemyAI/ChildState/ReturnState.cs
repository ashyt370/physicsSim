using UnityEngine;

public class ReturnState : EnemyStateBase
{
    public ReturnState(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        enemyAI.agent.SetDestination(enemyAI.startPosition);
    }
}
