using UnityEngine;

public class SkelletonMoveState : EnemyState
{
    private Skelleton enemy;
    public SkelletonMoveState(StateMachine _stateMachine, Entity _entity, string _animBoolName) : base(_stateMachine, _entity, _animBoolName)
    {
        enemy = _entity as Skelleton;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.moveSpeed * (enemy.flip ? 1 : -1), enemy.rb.linearVelocityY);
        if (enemy.isWalled() || !enemy.isGrounded())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
