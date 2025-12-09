using UnityEngine;

public class WallPMChange : MonoBehaviour
{
    public Collider wallCollider;
    public PhysicsMaterial normalMat;
    public PhysicsMaterial changedMat;

    public Renderer wallRender;
    public Color normalColor;
    public Color changedColor;

    public float changeInterval = 3f;
    private float timer = 0f;

    private bool isChanged = false;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= changeInterval)
        {
            timer = 0f;
            if(isChanged)
            {
                isChanged = false;
                wallCollider.material = normalMat;
                wallRender.material.color = normalColor;
            }
            else
            {
                isChanged = true;
                wallCollider.material = changedMat;
                wallRender.material.color = changedColor;
            }


        }
    }

}
