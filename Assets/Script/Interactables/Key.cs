using UnityEngine;

public class Key : Interactable
{
    public int keyID;

    public override void Interact(GameObject player)
    {

        UIManager.instance.HideInteractionHint();
        player.GetComponent<PlayerInteraction>().AddKey(keyID);
        Destroy(gameObject);
    }
}
