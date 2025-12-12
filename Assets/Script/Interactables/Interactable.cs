using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact(GameObject player);

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            UIManager.instance.ShowInteractionHint();

            other.GetComponent<PlayerInteraction>().currentInteractable = this;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.HideInteractionHint();

            other.GetComponent<PlayerInteraction>().currentInteractable = null;
        }
        
    }

}
