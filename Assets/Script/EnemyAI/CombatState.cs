using System.Collections.Generic;
using UnityEngine;

public class CombatState : EnemyStateBase
{
    private class Goal
    {
        public string name;
        public int priority;
        public bool isActive;
        public EnemyStateBase targetState;

        public Goal(string n, int p, EnemyStateBase t)
        {
            this.name = n;
            this.priority = p;
            this.targetState = t;
        }
    }
    private List<Goal> goals = new List<Goal>();

    public EnemyStateBase currentState;
    public CombatState(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter()
    {
        currentState = new PatrolState(enemyAI);
        currentState.Enter();

        goals.Add(new Goal("Survive", 100, new FleeState(enemyAI)));
        goals.Add(new Goal("Attack", 80, new ShootState(enemyAI)));
        goals.Add(new Goal("PickupAmmo", 60, new PickupAmmoState(enemyAI)));
        goals.Add(new Goal("ChasePlayer", 50, new ChasePlayerState(enemyAI)));
        goals.Add(new Goal("Patrol", 40, new PatrolState(enemyAI)));
        goals.Add(new Goal("Return", 30, new ReturnState(enemyAI)));

    }

    public override void Update()
    {
        if(forceState != null)
        {
            if(forceState.GetType() != currentState.GetType())
            {
                ChangeState(forceState);
                return;
            }            
        }
        currentState.Update();
        //condition change
        // Is enemy is dead
        if (enemyAI.HP <= 0)
        {
            ChangeState(new DeadState(enemyAI));
            return;
        }
        // Survive
        goals[0].isActive = enemyAI.isEnemyHPInDanger() && enemyAI.isPlayerInChaseRange();
        // attack
        goals[1].isActive = enemyAI.isPlayerInShootRange() && enemyAI.ammoAmount > 0;
        // pick up ammo
        goals[2].isActive = enemyAI.ammoAmount <= 0 && enemyAI.GetNearestAmmo() != null;
        // chase player
        goals[3].isActive = enemyAI.isPlayerInChaseRange() && !enemyAI.isEnemyHPInDanger();
        // patrol
        goals[4].isActive = currentState is ReturnState && enemyAI.agent.remainingDistance <= 0.5f;
        // return
        goals[5].isActive = (enemyAI.isEnemyHPInDanger() && enemyAI.isEnemyOutOfReturnRange()) || (currentState is PickupAmmoState && enemyAI.ammoAmount > 0);

        Goal activeGoal = null;
        int maxPriority = -1;

        foreach(Goal g in goals)
        {
            if(g.isActive && g.priority > maxPriority)
            {
                float prob = 1f;
                switch(g.name)
                {
                    case "PickupAmmo":
                        prob = 0.5f;
                        break;
                    case "Return":
                        prob = 0.3f;
                        break;
                    case "ChasePlayer":
                            prob = 0.8f;
                        break;
                }
                if(Random.value <= prob)
                {
                    maxPriority = g.priority;
                    activeGoal = g;
                }
                else
                {
                    Debug.Log("no chance to "+g.name);
                }
            }
        }

       if(activeGoal != null && activeGoal.targetState.GetType() != currentState.GetType())
        {
            ChangeState(activeGoal.targetState);
        }
    }


    public override void ChangeState(EnemyStateBase newstate)
    {
        currentState.Exit();
        currentState = newstate;
        currentState.Enter();
    }


    private EnemyStateBase forceState;

    public void ForceChangeState(EnemyStateBase newstate)
    {
        forceState = newstate;
    }

}
