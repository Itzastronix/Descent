using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float mouseSenstivity;
    private Rigidbody rb;
    private float RotationX = 0f;
    [SerializeField] Transform playerCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    // Update is called once per frame
    void Update()
    {
        CameraMovement();

    }

    void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    void HandlePlayerMovement()
    {
        float Moveforward = Input.GetAxis("Vertical");
        float MoveRight = Input.GetAxis("Horizontal");

        Vector3 moveDir = (transform.forward * Moveforward + transform.right * MoveRight).normalized;
        rb.linearVelocity = new Vector3(moveDir.x * playerSpeed, rb.linearVelocity.y, moveDir.z * playerSpeed);
    }

    void CameraMovement()
    {

        float MouseX = Input.GetAxis("Mouse X") * mouseSenstivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * mouseSenstivity * Time.deltaTime;

        RotationX -= MouseY;
        RotationX = Math.Clamp(RotationX, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(RotationX, 0f, 0f);

        transform.Rotate(Vector3.up * MouseX);
    }

}
