using System.Collections;
using UnityEngine;

public class Player : Entity
{
    #region Components
    public LayerMask enemyMask;
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
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    #endregion
    #region Listeners
    public void wallJump() => StartCoroutine(doJump());
    #endregion
    #region Unity
    protected override void Awake()
    {
        base.Awake();
        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
        jumpState = new PlayerJumpState(stateMachine, this, "Jump");
        dashState = new PlayerDashState(stateMachine, this, "Dash");
        wallState = new PlayerWallSlideState(stateMachine, this, "WallSlide");
        attackState = new PlayerAttackState(stateMachine, this, "Attack");
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();
        FlipController();
        DashController();
    }
    #endregion
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
        PlayerState currentState = (PlayerState)stateMachine.currentState;
        if ((rb.linearVelocityX > 0 && currentState.xInput > 0) && !flip)
        {
            Flip();
        } else if ((rb.linearVelocityX < 0 && currentState.xInput < 0) && flip)
        {
            Flip();
        }
    }
    IEnumerator doJump()
    {
        rb.AddForce(new Vector2(dashSpeed * (flip ? -1 : 1), jumpForce), ForceMode2D.Impulse);
        stateMachine.ChangeState(jumpState);
        stateMachine.currentState.ignoreInput = true;
        Flip();
        yield return new WaitForSeconds(.3f);
        stateMachine.currentState.ignoreInput = false;
    }
}
