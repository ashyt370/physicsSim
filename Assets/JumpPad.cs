using UnityEngine;

public class JumpPad : Interactable
{
    public float jumpForce = 70;
    public override void Interact(GameObject player)
    {
        player.GetComponent<Rigidbody>().AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
    }


}
