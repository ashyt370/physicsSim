
using System.Collections.Generic;
using UnityEngine;

public class ShootState : EnemyStateBase
{
    public ShootState(EnemyAI enemyAI) : base(enemyAI) { }
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
        enemyAI.EnemyShoot();
    }
}
