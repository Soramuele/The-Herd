

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalk : MonoBehaviour
{
    public InputAction InputActions;

    private InputAction m_moveAction;
    //private InputAction m_lookAction;
    private Vector2 m_moveInput;
    private Vector2 m_lookInput;
    //private Animator m_animator;
    private Rigidbody m_rigidbody;

    public float walkSpeed = 2.0f;
    public float runSpeed = 5.0f;
    public float rotateSpeed = 5.0f;

    //private void OnEnable()
    //{
    //    InputActions.FindActionMap("Player").enable();
        

    //}
    //private void OnDisable()
    //{
    //    InputActions.FindActionMap("Player").disable();
    //}
    
    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
        //m_lookAction = InputSystem.actions.FindAction("Look");
        m_moveAction.performed += ctx => m_moveInput = ctx.ReadValue<Vector2>();
        m_moveAction.canceled += ctx => m_moveInput = Vector2.zero;
        //m_lookAction.performed += ctx => m_lookInput = ctx.ReadValue<Vector2>();
        //m_lookAction.canceled += ctx => m_lookInput = Vector2.zero;
        //m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        m_moveInput = m_moveAction.ReadValue<Vector2>();
        //m_lookInput = m_lookAction.ReadValue<Vector2>();

    }
    private void FixedUpdate()
    {
        Walking();
        Rotating();
    }
    private void Walking()
    {
        Vector3 move = new Vector3(m_moveInput.x, 0, m_moveInput.y);
        if (move.magnitude > 1)
            move.Normalize();
        bool isRunning = Keyboard.current.leftShiftKey.isPressed;
        float speed = isRunning ? runSpeed : walkSpeed;
        Vector3 movement = move * speed * Time.fixedDeltaTime;
        m_rigidbody.MovePosition(transform.position + movement);
        // Update animator parameters
        float animationSpeed = isRunning ? 1.0f : 0.5f;
        //m_animator.SetFloat("Speed", move.magnitude * animationSpeed, 0.1f, Time.fixedDeltaTime);
    }
    private void Rotating()
    {
        if (m_lookInput.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(m_lookInput.x, m_lookInput.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime);
        }
    }
}
