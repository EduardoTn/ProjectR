using System.Collections;
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
    public Vector2[] AttackMovement;
    #endregion
    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    #endregion
    #region Listeners
    public bool isGrounded() => Physics2D.Raycast(groundCheck.position, Vector2.down, 0.15f, groundMask);
    public bool isWalled() => Physics2D.Raycast(wallCheck.position, Vector2.right, 0.1f, groundMask);
    public void wallJump() => StartCoroutine(doJump());
    public void animationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    #endregion
    void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
        jumpState = new PlayerJumpState(stateMachine, this, "Jump");
        dashState = new PlayerDashState(stateMachine, this, "Dash");
        wallState = new PlayerWallSlideState(stateMachine, this, "WallSlide");
        attackState = new PlayerAttackState(stateMachine, this, "Attack");
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        stateMachine.Initialize(idleState);
    }
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

    IEnumerator doJump()
    {
        rb.AddForce(new Vector2(dashSpeed * (flip ? -1 : 1), jumpForce), ForceMode2D.Impulse);
        stateMachine.ChangeState(jumpState);
        stateMachine.currentState.ignoreInput = true;
        yield return new WaitForSeconds(.3f);
        stateMachine.currentState.ignoreInput = false;
    }
}
