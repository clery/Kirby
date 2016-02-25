using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    private Rigidbody2D _rigidbody;
    [SerializeField]
    private LayerMask _isGround;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Vector2 _jumpForce;
    [SerializeField]
    private int _defaultAvailableJumps = 10;
    private int _availableJumps;
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _defaultSpeedTimer = 1f;
    private float _speedTimer = -1f;
    private float _direction = 0f;
    public bool Running { get; private set; }
    public bool Left { get; set; }
    public bool Right { get; set; }
    public float AxisH
    {
        get
        {
            return (0 + (Left || Input.GetAxisRaw("Horizontal") < 0 ? -1 : 0) + (Right || Input.GetAxisRaw("Horizontal") > 0 ? 1 : 0));
        }
    }
    private bool _isDead = false;
    public bool IsDead { get { return (_isDead); } private set { _isDead = value; } }

    public delegate void DeathAction(Controller character);
    public static event DeathAction OnDeath;

#if UNITY_EDITOR
    static float lastHAxis = 0f;
#endif

    void OnEnable()
    {
        OnDeath += Handle_OnDeath;
    }

    void OnDisable()
    {
        OnDeath -= Handle_OnDeath;
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
    }

    public void OnButtonReleased()
    {
        _speedTimer = _defaultSpeedTimer;
        Running = false;
    }

    void Update()
    {
        _speedTimer -= Time.deltaTime;
        _animator.SetBool("Grounded", IsGrounded());
    }

    // Update is called once per frame
    void FixedUpdate () {
        Move();
        if (IsGrounded())
            _availableJumps = _defaultAvailableJumps;
#if UNITY_EDITOR
        if (Input.GetButtonDown("Jump"))
            Jump();
        if (Input.GetAxisRaw("Horizontal") == 0 && lastHAxis != 0)
            OnButtonReleased();
        lastHAxis = Input.GetAxisRaw("Horizontal");
#endif
    }

    void Move()
    {
        if (!IsDead)
        {
            if (AxisH != 0)
            {
                if (_speedTimer >= 0f)
                    Running = true;
                _rigidbody.velocity = new Vector2(AxisH * _speed * (Running ? 2 : 1), _rigidbody.velocity.y);
                transform.localScale = new Vector3(AxisH, transform.localScale.y, transform.localScale.z);
            }
            _animator.SetFloat("HSpeed", Mathf.Abs(_rigidbody.velocity.x));
        }
    }

    public void Jump()
    {
        if (!IsDead && (IsGrounded() || _availableJumps > 0))
        {
            if (!IsGrounded())
                _availableJumps--;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(_jumpForce, ForceMode2D.Impulse);
            _animator.SetTrigger("Jump");
        }
    }

    public bool IsGrounded()
    {
        Debug.DrawRay(transform.position, Vector2.down * 0.05f, Color.red);
        return (Physics2D.Raycast(transform.position, Vector2.down, 0.05f, _isGround));
    }

    void Handle_OnDeath(Controller character)
    {
        IsDead = true;
        _rigidbody.gravityScale = 0;
        _rigidbody.velocity = Vector2.zero;
        _animator.SetTrigger("Dead");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Killer") && OnDeath != null)
            OnDeath(this);
    }
}
