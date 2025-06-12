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
    public Vector2 movemment { get; private set; }
    public PlayerCommands commands { get; private set; }
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
    public PlayerBlackHoleState blackHoleState { get; private set; }
    public PlayerDieState deathState { get; private set; }
    #endregion
    #region Listeners
    public void wallJump() => StartCoroutine(doJump());
    public Collider2D[] GetEntity() => Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
    #endregion
    #region Unity
    protected override void Awake()
    {
        base.Awake();
        commands = new PlayerCommands();
        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
        jumpState = new PlayerJumpState(stateMachine, this, "Jump");
        dashState = new PlayerDashState(stateMachine, this, "Dash");
        wallState = new PlayerWallSlideState(stateMachine, this, "WallSlide");
        attackState = new PlayerAttackState(stateMachine, this, "Attack");
        hitState = new PlayerHitState(stateMachine, this, "Hit");
        counterState = new PlayerCounterState(stateMachine, this, "Counter");
        blackHoleState = new PlayerBlackHoleState(stateMachine, this, "Jump");
        deathState = new PlayerDieState(stateMachine, this, "Die");
    }
    private void OnEnable()
    {
        commands.Enable();
        commands.Player.Move.performed += ctx => movemment = ctx.ReadValue<Vector2>();
        commands.Player.Move.canceled += ctx => movemment = Vector2.zero;
    }
    private void OnDisable()
    {
        commands.Disable();
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
        if (!isDead)
        {
            CounterController();
            DashController();
            BlackHoleController();
        }
    }
    #endregion
    #region Controllers
    private void DashController()
    {
        if (commands.Player.Dash.WasPressedThisFrame() && skill.dash.CanUseSkill())
        {
            isKnocked = false;
            stateMachine.ChangeState(dashState);
            skill.dash.UseSkill();
        }
    }
    private void CounterController()
    {
        if (commands.Player.Parry.WasPressedThisFrame() && skill.parry.CanUseSkill())
        {
            stateMachine.ChangeState(counterState);
            skill.parry.UseSkill();
        }
    }
    private void BlackHoleController()
    {
        if (commands.Player.Ultimate.WasPressedThisFrame() && skill.blackHole.CanUseSkill())
        {
            stateMachine.ChangeState(blackHoleState);
        }
    }
    public void ExitBlackHole()
    {
        stateMachine.ChangeState(idleState);
        MakeTransparent(false);
    }
    public override void Damage(bool isHeavy, Entity attacker, float _damage)
    {
        if (stateMachine.currentState is not PlayerDashState)
        {
            base.Damage(isHeavy, attacker, _damage);
        }
    }
    protected override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
    }
    public bool isEnemyinArea()
    {
        Collider2D[] colliders = GetEntity();
        foreach (var hit in colliders)
        {
            var enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
                return true;
        }
        return false;
    }
    #endregion
    #region Actions
    IEnumerator doJump()
    {
        rb.linearVelocity = new Vector2((dashSpeed/2) * (flip ? -1 : 1), jumpForce + speed);
        stateMachine.ChangeState(jumpState);
        stateMachine.currentState.ignoreInput = true;
        Flip();
        yield return new WaitForSeconds(.3f);
        stateMachine.currentState.ignoreInput = false;
    }
    #endregion
}
