using UnityEngine;
using UnityEngine.AI;

public class PickupAmmoState : EnemyStateBase
{
    public PickupAmmoState(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter()
    {
        // get all available ammo in the level
        GameObject nearestAmmo = enemyAI.GetNearestAmmo();
        // if no ammo
        if(nearestAmmo == null)
        {
            return;
        }

        NavMeshHit hit;
        if(NavMesh.SamplePosition(nearestAmmo.transform.position,out hit,5f,NavMesh.AllAreas))
        {
            enemyAI.agent.SetDestination(hit.position);
        }
        else
        {
            Debug.LogError("Nearest ammo is not available on navmesh");
        }
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
