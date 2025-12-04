using UnityEngine;

public class KnockBall : MonoBehaviour
{
    public float hitForce = 10f;
    public float hitUpForce = 10f;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Vector3 forceDirection = (transform.position - collision.transform.position).normalized;
            forceDirection.y = hitUpForce;

            collision.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * hitForce, ForceMode.Impulse);              
        }
    }
}
