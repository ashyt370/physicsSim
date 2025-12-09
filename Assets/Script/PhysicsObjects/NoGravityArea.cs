using UnityEngine;

public class NoGravityArea : MonoBehaviour
{
    public float upForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Rigidbody>())
        {
            // Physics properties are changed via scripts
            other.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * upForce, ForceMode.Acceleration);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            // Physics properties are changed via scripts
            other.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
