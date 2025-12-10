using UnityEngine;

public class DeadState : EnemyStateBase
{
    public DeadState(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter()
    {
        enemyAI.agent.isStopped = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
