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
