using UnityEngine;

public abstract class EnemyStateBase
{
    public EnemyAI enemyAI;

    public EnemyStateBase(EnemyAI enemy)
    {
        this.enemyAI = enemy;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void Update()
    {
        
    }
    public virtual void Exit()
    {

    }


}
