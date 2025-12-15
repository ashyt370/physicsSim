using UnityEngine;

public class BounceWall : MonoBehaviour
{
    public float bounceForce = 10f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb == null) return;

            Vector3 dir = collision.transform.position - transform.position;
            dir.y = 5f;
            dir.Normalize();

            rb.linearVelocity = Vector3.zero;

            rb.AddForce(dir * bounceForce, ForceMode.Impulse);

            Debug.Log("Bounce");
        }
    }
}
