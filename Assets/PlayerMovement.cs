using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction moveAction;
    private InputAction lookAction;

    private Rigidbody rb;

    public float moveForce = 20f;
    public float maxSpeed = 5f;
    public float cameraRotationSpeed = 90f;
    public float jumpForce = 50f;

    private Vector2 moveDirection;
    private Vector2 mouseMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        moveAction = inputActions.FindActionMap("Player").FindAction("Move");
        lookAction = inputActions.FindActionMap("Player").FindAction("Look");

        moveAction.Enable();
        lookAction.Enable();

        inputActions.FindActionMap("Player").FindAction("Jump").performed += Jump;
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if(isGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
    }

    private void FixedUpdate()
    {
        moveDirection = moveAction.ReadValue<Vector2>();
        mouseMove = lookAction.ReadValue<Vector2>();

        // Player Movement
        Vector3 dir = transform.forward * moveDirection.y + transform.right * moveDirection.x;

        if(rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(dir.normalized * moveForce, ForceMode.Force);
        }

        // Player Rotation
        float fx = mouseMove.x * cameraRotationSpeed * Time.fixedDeltaTime;

        transform.Rotate(0, fx, 0);

    }

    private bool isGround = false;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGround = false;
        }
    }


}
