using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private GameObject InteractionHint;

    public static UIManager instance;

    [SerializeField]
    private TextMeshProUGUI bulletAmountTMP;

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

    public void UpdateBulletAmount(float f)
    {
        bulletAmountTMP.text = f.ToString();
    }
}
