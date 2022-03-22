using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D _rb;
    PlayerStats _stats;

    Vector2 _movement;

    [SerializeField] float accelerationSpeed;
    [SerializeField] float maxMovementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float groundLinearDrag;
    [SerializeField] float airLinearDrag;
    bool jumpButton;
    bool isJumpPressed;

    bool _changingDirection => (_rb.velocity.x > 0f && _movement.x < 0f) || (_rb.velocity.x < 0f && _movement.x > 0f);

    void Awake()
    {
        _stats = GetComponent<PlayerStats>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        isJumpPressed = Input.GetButtonDown("Jump");
        //ApplyJump();
    }

    void FixedUpdate()
    {
        ApplyMovement();
        //ApplyGroundLinearDrag();
        if (isJumpPressed && _stats.IsGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void ApplyMovement()
    {
        _rb.AddForce(_movement * accelerationSpeed, ForceMode2D.Force);

        if (Mathf.Abs(_rb.velocity.x) > maxMovementSpeed)
        {
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * maxMovementSpeed, _rb.velocity.y);
        }
    }

    private void ApplyGroundLinearDrag()
    {
        if (Mathf.Abs(_movement.x) < .4f || _changingDirection)
        {
            _rb.drag = groundLinearDrag;
        }
        else
        {
            _rb.drag = 0;
        }
    }

    private void ApplyJump()
    {
        if (Input.GetButtonDown("Jump") && _stats.IsGrounded)
        {

            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
            jumpButton = false;
        }
    }
}
