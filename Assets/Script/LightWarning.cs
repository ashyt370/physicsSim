using UnityEngine;

public class LightWarning : MonoBehaviour
{
    public Light light; 
    public float minIntensity = 0.5f;
    public float maxIntensity = 2f;
    public float flickerSpeed = 10f; 

    private float targetIntensity;

    void Start()
    {
        light = GetComponent<Light>();

        targetIntensity = light.intensity;
    }

    void Update()
    {
        light.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(Time.time * flickerSpeed, 1f));
    }
}
