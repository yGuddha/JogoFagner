using UnityEngine;
using UnityEngine.InputSystem;

public class Character2DController : MonoBehaviour
{
    Rigidbody2D _rb;
    BoxCollider2D _bcoll;
    PlayerInputActions _input;

    Vector2 _axisInput;

    [Header("Movement")]
    [SerializeField] float movementSpeed;
    [SerializeField] float maxMovementSpeed;
    [SerializeField] float jumpForce;

    [Header("Linear Drag")]
    [SerializeField] float groundLinearDrag;
    [SerializeField] float airLinearDrag;

    [Header("Timers")]
    [SerializeField] float coyoteTimer;
    [SerializeField] float jumpBufferTimer;

    [Header("Counters")]
    [SerializeField] float coyoteCounter;
    [SerializeField] float jumpBufferCounter;

    [Header("Ground Collision Box")]
    [SerializeField] Vector2 groundBoxSize;
    [SerializeField] Vector2 groundBoxPos;
    [SerializeField] Vector2 groundBoxOffset;

    bool _isGrounded;
    bool _isTurningSide;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bcoll = GetComponent<BoxCollider2D>();
        _input = new PlayerInputActions();
    }

    void OnEnable()
    {
        _input.Enable();

        _input.Movement.Axis.performed += ReadAxisInput;
        _input.Movement.Axis.canceled += ResetAxisInput;
    }
    void OnDisable()
    {
        _input.Disable();

        _input.Movement.Axis.performed -= ReadAxisInput;
        _input.Movement.Axis.canceled -= ResetAxisInput;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundBoxPos + groundBoxOffset, groundBoxSize);
    }

    private void ApplyMovement()
    {
        _rb.AddForce(_axisInput * movementSpeed, ForceMode2D.Force);

        if (Mathf.Abs(_rb.velocity.x) > maxMovementSpeed)
            _rb.velocity = new Vector2(maxMovementSpeed * Mathf.Sign(_rb.velocity.x), _rb.velocity.y);
    }

    private void ReadAxisInput(InputAction.CallbackContext ctx) => _axisInput = ctx.ReadValue<Vector2>();
    private void ResetAxisInput(InputAction.CallbackContext ctx) => _axisInput = Vector2.zero;

}
