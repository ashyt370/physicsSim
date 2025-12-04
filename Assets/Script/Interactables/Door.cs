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
            UIManager.instance.ShowKeyRequiredHint();
        }
    }

    public void OpenDoor()
    {
        gameObject.GetComponent<Animator>().SetTrigger("OpenDoor");
    }


}
