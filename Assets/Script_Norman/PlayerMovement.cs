using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference moveInput;
    [SerializeField] private InputActionReference jumpInput;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxSlopeAngle = 45f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.25f;
    [SerializeField] private LayerMask groundLayer;

    private Camera cam;

    private Rigidbody rb;
    private Vector2 move;
    private bool isGrounded;

    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // critical for FPS
    }

    void OnEnable()
    {
        moveInput.action.Enable();
        jumpInput.action.Enable();
    }

    void OnDisable()
    {
        moveInput.action.Disable();
        jumpInput.action.Disable();
    }

    void Update()
    {
        move = moveInput.action.ReadValue<Vector2>();

        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        if (jumpInput.action.WasPressedThisFrame() && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 moveDir =
            cam.transform.right * move.x +
            cam.transform.forward * move.y;
       
        Vector3 velocity = rb.linearVelocity;
        Vector3 targetVelocity = new Vector3(moveDir.normalized.x, 0.0f, moveDir.normalized.z) * moveSpeed;

        rb.linearVelocity += targetVelocity;
        //rb.linearVelocity = new Vector3(
        //    targetVelocity.x,
        //    velocity.y,
        //    targetVelocity.z
        //);
        
        rb.linearVelocity -= new Vector3(rb.linearVelocity.x * .2f, 0, rb.linearVelocity.z * .2f);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}