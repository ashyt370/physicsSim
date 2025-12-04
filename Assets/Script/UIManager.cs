using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI Elements")]
    [SerializeField]
    private GameObject InteractionHint;
    [SerializeField]
    private GameObject KeyRequiredHint;
    [SerializeField]
    private TextMeshProUGUI bulletAmountTMP;
    [SerializeField]
    private GameObject loseScreen;

    private void Awake()
    {
        HideInteractionHint();
        HideLoseScreen();
        HideKeyRequiredHint();

        instance = this;
    }

    public void ShowKeyRequiredHint()
    {
        Debug.Log("key");
        KeyRequiredHint.SetActive(true);

        StartCoroutine("WaitForTime");
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(2f);
        HideKeyRequiredHint();
    }

    public void HideKeyRequiredHint()
    {
        KeyRequiredHint.SetActive(false);
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

    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void HideLoseScreen()
    {
        loseScreen.SetActive(false);
        Time.timeScale = 1;
    }
}
