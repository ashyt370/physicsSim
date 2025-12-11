using UnityEngine;

public class BulletPickup : Interactable
{
    public int bulletAmount = 10;
    public override void Interact(GameObject player)
    {
        player.GetComponent<PlayerMovement>().AddBullet(bulletAmount);
        UIManager.instance.HideInteractionHint();
        Destroy(gameObject);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.ShowInteractionHint();

            other.GetComponent<PlayerInteraction>().currentInteractable = this;
        }
        else if(other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            other.GetComponent<EnemyAI>().ammoAmount += bulletAmount;
        }
    }
}
