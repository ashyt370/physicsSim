using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private List<int> keyList = new List<int>();

    private InputActionAsset inputActions;

    [HideInInspector]
    public Interactable currentInteractable;

    public float currentHP;
    private float maxHP = 100f;

    public void Start()
    {
        currentHP = maxHP;
        UIManager.instance.UpdateHP(currentHP / maxHP);
    }
    public void TakeDamage(float d)
    {
        currentHP -= d;
        UIManager.instance.UpdateHP(currentHP / maxHP);
        if(currentHP <= 0)
        {
            UIManager.instance.ShowLoseScreen();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(20);
        }
    }

    private void Awake()
    {
        inputActions = gameObject.GetComponent<PlayerMovement>().inputActions;
        inputActions.FindActionMap("Player").FindAction("Interact").performed += Interact;
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        Debug.Log("Pressed E");
        if(currentInteractable)
        {
            currentInteractable.Interact(gameObject);
        }
    }

    public void AddKey(int keyID)
    {
        keyList.Add(keyID);
    }

    public bool IfContainsKey(int keyID)
    {
        if (keyList.Contains(keyID))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
