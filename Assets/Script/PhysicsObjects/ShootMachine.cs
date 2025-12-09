using UnityEngine;

public class ShootMachine : MonoBehaviour
{

    public GameObject projectilePrefab;
    public Transform targetPoint;

    public float shootTime = 1f;

    public float shootInterval = 2f;
    private float timer = 0f;

    private bool isTrigger = false;

    private void Update()
    {
        if(isTrigger)
        {
            timer += Time.deltaTime;
            if (timer >= shootInterval)
            {
                timer = 0;
                ShootProjectile();
            }
        }
    }

    private void ShootProjectile()
    {
        GameObject obj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Vector3 distance = targetPoint.position - transform.position;
        Vector3 horizontalDistance = new Vector3(distance.x, 0, distance.z);
        float horizontalVelocity = horizontalDistance.magnitude / shootTime;

        float verticalVelocity = (distance.y - 0.5f * Physics.gravity.y * shootTime * shootTime) / shootTime;

        Vector3 finalVelocity = horizontalDistance.normalized * horizontalVelocity;
        finalVelocity.y = verticalVelocity;

        obj.GetComponent<Rigidbody>().linearVelocity = finalVelocity;

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = false;
        }
    }
}
