using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject playerRespawnTransform;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().ChangeNewResetPoint(playerRespawnTransform.transform.position, playerRespawnTransform.transform.rotation);
        }
    }
}
