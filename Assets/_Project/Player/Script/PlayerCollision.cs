using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerStats _stats;
    BoxCollider2D _coll;

    Vector2 _groundCheckBoxPos;
    Vector2 _groundCheckBoxSize;
    [SerializeField] float groundBoxOffset;
    [SerializeField] LayerMask groundLayerMask;

    void Awake()
    {
        _coll = GetComponent<BoxCollider2D>();
        _stats = GetComponent<PlayerStats>();
    }

    void Start()
    {
        _groundCheckBoxPos = new Vector2();
        _groundCheckBoxSize = new Vector2(_coll.bounds.size.x * .99f, .05f);
    }

    void Update()
    {
        _groundCheckBoxPos.x = _coll.bounds.center.x;
        _groundCheckBoxPos.y = _coll.bounds.center.y - (_coll.bounds.size.y * .5f) - groundBoxOffset;
    }

    void FixedUpdate()
    {
        _stats.IsGrounded = Physics2D.OverlapBox(_groundCheckBoxPos, _groundCheckBoxSize, 0, groundLayerMask);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_groundCheckBoxPos, _groundCheckBoxSize);
    }
}
