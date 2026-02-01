using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private InputActionReference lookInput;
    [SerializeField] private Transform cameraPivotTransform;
    [SerializeField] private float mouseSensitivity = 100f;

    private float xRotation;
    private float yRotation;
    private float currentX;
    private float currentY;

    void OnEnable()
    {
        lookInput.action.Enable();
    }

    void OnDisable()
    {
        lookInput.action.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        Vector2 look = lookInput.action.ReadValue<Vector2>();

        float mouseX = look.x * mouseSensitivity;
        float mouseY = look.y * mouseSensitivity;
        
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraPivotTransform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        /*currentX = Mathf.Lerp(currentX, xRotation, Time.deltaTime);
        currentY = Mathf.Lerp(currentY, yRotation, Time.deltaTime);*/


        // Yaw
        //transform.Rotate(Vector3.up * mouseX);
    }
}