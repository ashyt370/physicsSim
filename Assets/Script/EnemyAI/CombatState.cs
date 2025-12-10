using UnityEngine;

public class CombatState : EnemyStateBase
{
    public EnemyStateBase currentState;
    public CombatState(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter()
    {
        currentState = new PatrolState(enemyAI);
        currentState.Enter();
    }

    public override void Update()
    {
        //condition change

        // Is player is in range and enemy is not in danger
        if (enemyAI.isPlayerInChaseRange() && !enemyAI.isEnemyHPInDanger())
        {
            currentState.Exit();
            currentState = new ChasePlayerState(enemyAI);
            currentState.Enter();
        }

        // Is enemy is dead
        else if (enemyAI.HP <= 0)
        {
            currentState.Exit();
            currentState = new DeadState(enemyAI);
            currentState.Enter();
        }

        // If Enemy is in danger and Player is far enough
        else if(enemyAI.isEnemyHPInDanger() && enemyAI.isEnemyOutOfReturnRange())
        {
            currentState.Exit();
            currentState = new ReturnState(enemyAI);
            currentState.Enter();
        }

        // If Enemy is in danger and Player is still nearby
        else if (enemyAI.isEnemyHPInDanger() && !enemyAI.isEnemyOutOfReturnRange())
        {
            currentState.Exit();
            currentState = new FleeState(enemyAI);
            currentState.Enter();
        }

        else if(enemyAI.ammoAmount >= 0 && enemyAI.isPlayerInChaseRange())
        {
            currentState.Exit();
            currentState = new ShootState(enemyAI);
            currentState.Enter();
        }

        else if (enemyAI.ammoAmount == 0 && !enemyAI.isPlayerInChaseRange())
        {
            currentState.Exit();
            currentState = new PickupAmmoState(enemyAI);
            currentState.Enter();
        }

    }

    public override void Exit()
    {

    }

}
