using UnityEngine;

public class ItemFloating : MonoBehaviour
{

    public float floatValue = 0.2f;
    public float speed = 2f;

    private Vector3 dir = Vector3.up; 
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float y = startPos.y + Mathf.Sin(Time.time * speed) * floatValue;
        transform.position = startPos + dir * (y - startPos.y);
    }
}
