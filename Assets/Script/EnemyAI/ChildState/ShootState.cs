
using System.Collections.Generic;
using UnityEngine;

public class ShootState : EnemyStateBase
{
    public ShootState(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter()
    {
        enemyAI.agent.isStopped = true;

        Vector3 direction = (enemyAI.target.position - enemyAI.transform.position).normalized;
        direction.y = 0f;
        enemyAI.transform.rotation = Quaternion.LookRotation(direction);
    }

    public override void Exit()
    {
        enemyAI.agent.isStopped = false;
    }

    private float shootInterval = 1f;
    private float shootTime = 0f;
    public override void Update()
    {
        shootTime += Time.deltaTime;

        if(shootTime >= shootInterval)
        {
            enemyAI.EnemyShoot();
            shootTime = 0;
        }
    }
}
