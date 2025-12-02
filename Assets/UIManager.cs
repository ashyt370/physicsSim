using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private GameObject InteractionHint;

    public static UIManager instance;

    private void Awake()
    {
        HideInteractionHint();

        instance = this;
    }
    public void ShowInteractionHint()
    {
        InteractionHint.SetActive(true);
    }

    public void HideInteractionHint()
    {
        InteractionHint.SetActive(false);
    }
}
