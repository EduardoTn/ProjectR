using UnityEngine;

public class SkelletonMoveState : SkelletonBaseState
{
    public SkelletonMoveState(StateMachine _stateMachine, Skelleton _entity, string _animBoolName) : base(_stateMachine, _entity, _animBoolName)
    {
    }
    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.moveSpeed * (enemy.flip ? 1 : -1), enemy.rb.linearVelocityY);
        if (enemy.isPlayerDetected())
            stateMachine.ChangeState(enemy.battleState);
        if (enemy.isWalled() || !enemy.isGrounded())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
