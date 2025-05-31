using UnityEngine;

public class Skelleton : Enemy
{
    #region States
    public SkelletonIdleState idleState { get; private set; }
    public SkelletonMoveState moveState { get; private set; }
    public SkelletonBattleState battleState { get; private set; }
    public SkelletonAttackState attackState { get; private set; }
    public SkelletonStunState stunState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new SkelletonIdleState(stateMachine, this, "Idle");
        moveState = new SkelletonMoveState(stateMachine, this, "Move");
        battleState = new SkelletonBattleState(stateMachine, this, "Move");
        attackState = new SkelletonAttackState(stateMachine, this, "Attack");
        stunState = new SkelletonStunState(stateMachine, this, "Stun");
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    { 
        base.Update();
    }

    public override void ReceiveCounterAttack()
    {
        stateMachine.ChangeState(stunState);
    }
}
