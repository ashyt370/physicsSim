using UnityEngine;

public class MaterialFlicker : MonoBehaviour
{

    private MeshRenderer meshRenderer;
    private Color color1;
    public Color color2;
    public float speed = 2f;

    private Material material;

    public bool isFlicker = false;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        color1 = material.color;
    }

    void Update()
    {
        if(isFlicker)
        {
            float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
            material.color = Color.Lerp(color1, color2, t);
        }
        
    }
}
