using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 70;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);
        }
    }
}
