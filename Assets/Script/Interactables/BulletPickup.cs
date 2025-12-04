using UnityEngine;

public class BulletPickup : Interactable
{
    public int bulletAmount = 10;
    public override void Interact(GameObject player)
    {
        player.GetComponent<PlayerMovement>().AddBullet(bulletAmount);
        Destroy(gameObject);
    }
}
