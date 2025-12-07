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
    public float shootForce = 100f;
    public Transform shootPoint;

    private Vector2 moveDirection;
    private Vector2 mouseMove;

    public GameObject bulletPrefab;

    public int bulletAmount = 10;

    private Vector3 originPosition;
    private Quaternion originRotation;

    public float resetFallY = -20;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        moveAction = inputActions.FindActionMap("Player").FindAction("Move");
        lookAction = inputActions.FindActionMap("Player").FindAction("Look");

        moveAction.Enable();
        lookAction.Enable();

        inputActions.FindActionMap("Player").FindAction("Jump").performed += Jump;
        inputActions.FindActionMap("Player").FindAction("Attack").performed += Shoot;
    }

    private void Start()
    {
        UIManager.instance.UpdateBulletAmount(bulletAmount);

        originPosition = gameObject.transform.position;
        originRotation = gameObject.transform.rotation;
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        if(bulletAmount > 0)
        {
            if(bulletPrefab && shootPoint)
            {
                GameObject b = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation, null);

                b.GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
                AddBullet(-1);
                Destroy(b, 5f);
            }
        
        }
        else
        {
            Debug.Log("no bullet");
        }
        
    }

    public void AddBullet(int amount)
    {
        bulletAmount += amount;
        UIManager.instance.UpdateBulletAmount(bulletAmount);
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if(isGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
    }

    private void Update()
    {
        if(gameObject.transform.position.y <= resetFallY)
        {   
            ResetPlayer();
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


    private void ResetPlayer()
    {
        Debug.Log("reset player");

        transform.position = originPosition;
        transform.rotation = originRotation;
    }

    public void ChangeNewResetPoint(Vector3 v, Quaternion q)
    {
        originRotation = q;
        originPosition = v;
    }


}
