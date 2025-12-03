using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 70;

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Rigidbody>())
        {
            other.GetComponent<Rigidbody>().AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        }
    }



}
