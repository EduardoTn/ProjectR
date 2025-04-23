using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundMask;
    public bool flip = true;
    [Header("move info")]
    public float speed;
    public float jumpForce;
    public float dashDuration = 0.5f;
    private float dashInCooldown = 0f;
    public float dashCooldown = 1f;
    public float dashSpeed = 15;
    #endregion
    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    #endregion
    void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
        jumpState = new PlayerJumpState(stateMachine, this, "Jump");
        dashState = new PlayerDashState(stateMachine, this, "Dash");
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Update();
        FlipController();
        DashController();
    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
    }
    public bool isGrounded() => Physics2D.Raycast(groundCheck.position, Vector2.down, 0.15f, groundMask);
    public void Flip()
    {
        flip = !flip;
        transform.Rotate(0f, 180f, 0f);
    }
    private void DashController()
    {
        dashInCooldown -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashInCooldown < 0)
        {
            dashInCooldown = dashCooldown;
            stateMachine.ChangeState(dashState);

        }
    }
    public void FlipController()
    {
        if (rb.linearVelocityX > 0 && !flip)
        {
            Flip();
        } else if (rb.linearVelocityX < 0 && flip)
        {
            Flip();
        }
    }
}
