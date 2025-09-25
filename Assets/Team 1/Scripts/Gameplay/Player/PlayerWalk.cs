using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalk : MonoBehaviour
{
    public InputActionAsset inputActions; // Reference your Input Actions asset in the Inspector

    private InputAction m_moveAction;
    private InputAction m_lookAction;

    private Vector2 m_moveInput;
    private Vector2 m_lookInput;

    private CharacterController m_controller;
    private Camera m_camera;

    [Header("Movement Settings")]
    public float walkSpeed = 2.0f;
    public float runSpeed = 5.0f;
    public float rotateSpeed = 10.0f;

    [Header("Gravity Settings")]
    public float gravity = -9.81f;
    private float verticalVelocity = 0f;

    private void Awake()
    {
        // Get Input Actions
        m_moveAction = inputActions.FindActionMap("Player").FindAction("Move");
        m_lookAction = inputActions.FindActionMap("Player").FindAction("Look");

        // Enable input actions
        m_moveAction.Enable();
        m_lookAction.Enable();

        // Cache components
        m_controller = GetComponent<CharacterController>();
        m_camera = Camera.main;
    }

    private void Update()
    {
        // Read inputs
        m_moveInput = m_moveAction.ReadValue<Vector2>();
        m_lookInput = m_lookAction.ReadValue<Vector2>();

        Move();
        Rotate();
    }

    private void Move()
    {
        // Convert input to world space relative to camera
        Vector3 forward = m_camera.transform.forward;
        Vector3 right = m_camera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * m_moveInput.y + right * m_moveInput.x;

        if (move.magnitude > 1f)
            move.Normalize();

        bool isRunning = Keyboard.current.leftShiftKey.isPressed;
        float speed = isRunning ? runSpeed : walkSpeed;

        // Apply gravity
        if (m_controller.isGrounded)
        {
            verticalVelocity = -1f; // keep grounded
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 movement = move * speed + Vector3.up * verticalVelocity;
        m_controller.Move(movement * Time.deltaTime);
    }

    private void Rotate()
    {
        // Rotate with look input (if using mouse/gamepad look)
        if (m_lookInput.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(m_lookInput.x, m_lookInput.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
        // Or rotate to face movement direction
        else if (m_moveInput.sqrMagnitude > 0.01f)
        {
            Vector3 direction = new Vector3(m_moveInput.x, 0f, m_moveInput.y);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
    }
}
