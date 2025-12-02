using UnityEngine;

public class Door : Interactable
{
    // the keyID required
    public int keyID;

    public override void Interact(GameObject player)
    {
        UIManager.instance.HideInteractionHint();

        if (player.GetComponent<PlayerInteraction>().IfContainsKey(keyID))
        {
            OpenDoor();
        }
        else
        {
            Debug.Log("Need Key£º " + keyID);
        }
    }

    public void OpenDoor()
    {
        Destroy(gameObject);
    }


}
