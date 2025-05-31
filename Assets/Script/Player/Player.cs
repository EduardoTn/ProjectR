using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : Entity
{
    #region Components
    public float counterAttackDuration = .2f;
    public LayerMask enemyMask;
    public SkillManager skill;
    [Header("Move info")]
    public float speed;
    public float jumpForce;
    public float dashDuration = 0.5f;
    public float dashSpeed = 15;
    public Vector2[] attackMovement;
    #endregion
    #region States
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerHitState hitState { get; private set; }
    public PlayerCounterState counterState { get; private set; }
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
        hitState = new PlayerHitState(stateMachine, this, "Hit");
        counterState = new PlayerCounterState(stateMachine, this, "Counter");
    }
    protected override void Start()
    {
        base.Start();
        skill = SkillManager.instance;
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();
        CounterController();
        DashController();
    }
    #endregion
    #region Controllers
    private void DashController()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && skill.dash.CanUseSkill())
        {
            isKnocked = false;
            stateMachine.ChangeState(dashState);
            skill.dash.UseSkill();
        }
    }
    private void CounterController()
    {
        if (Input.GetKey(KeyCode.Q) && skill.parry.CanUseSkill())
        {
            stateMachine.ChangeState(counterState);
            skill.parry.UseSkill();
        }
    }
    public override void Damage(bool isHeavy, Entity attacker)
    {
        if (stateMachine.currentState is not PlayerDashState)
        {
            base.Damage(isHeavy, attacker);
        }
    }
    #endregion
    #region Actions
    IEnumerator doJump()
    {
        rb.linearVelocity = new Vector2(dashSpeed * (flip ? -1 : 1), jumpForce + speed);
        stateMachine.ChangeState(jumpState);
        stateMachine.currentState.ignoreInput = true;
        Flip();
        yield return new WaitForSeconds(.3f);
        stateMachine.currentState.ignoreInput = false;
    }
    #endregion
}
