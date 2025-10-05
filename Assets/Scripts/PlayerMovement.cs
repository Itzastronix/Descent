using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float crouchSpeed = 2f;
    [SerializeField] private float mouseSensitivity = 150f;
    [SerializeField] private float cameraSmoothTime = 8f;

    [Header("Crouch Settings")]
    [SerializeField] private float crouchHeight = 0.8f;
    [SerializeField] private float standHeight = 1.6f;
    [SerializeField] private float crouchTransitionSpeed = 6f;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("References")]
    [SerializeField] private Transform playerCamera;

    private Rigidbody rb;
    private float rotationX = 0f;
    private bool isCrouching = false;
    private float targetHeight;
    private float heightVelocity = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        targetHeight = standHeight;
    }

    void Update()
    {
        HandleCameraMovement();
        HandleCrouch();
    }

    void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        float moveForward = Input.GetAxis("Vertical");
        float moveRight = Input.GetAxis("Horizontal");

        float currentSpeed = isCrouching ? crouchSpeed : playerSpeed;

        Vector3 moveDir = (transform.forward * moveForward + transform.right * moveRight).normalized;
        rb.linearVelocity = new Vector3(moveDir.x * currentSpeed, rb.linearVelocity.y, moveDir.z * currentSpeed);
    }

    private void HandleCameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        Quaternion targetRotation = Quaternion.Euler(rotationX, 0f, 0f);
        playerCamera.localRotation = Quaternion.Slerp(playerCamera.localRotation, targetRotation, cameraSmoothTime * Time.deltaTime);

        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleCrouch()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            isCrouching = !isCrouching;
            targetHeight = isCrouching ? crouchHeight : standHeight;
        }

        Vector3 camPos = playerCamera.localPosition;
        camPos.y = Mathf.SmoothDamp(camPos.y, targetHeight, ref heightVelocity, 1f / crouchTransitionSpeed);
        playerCamera.localPosition = camPos;
    }
}
