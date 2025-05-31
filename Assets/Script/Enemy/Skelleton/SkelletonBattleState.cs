using UnityEngine;

public class SkelletonBattleState : SkelletonBaseState
{
    public SkelletonBattleState(StateMachine _stateMachine, Skelleton _entity, string _animBoolName) : base(_stateMachine, _entity, _animBoolName)
    {
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
        if (player.transform.position.x > enemy.transform.position.x && !enemy.flip)
        {
            enemy.Flip();
        }
        else if (player.transform.position.x < enemy.transform.position.x && enemy.flip)
        {
            enemy.Flip();
        }
        enemy.SetVelocity(enemy.moveSpeed * (enemy.flip ? 1 : -1), enemy.rb.linearVelocityY);

        if (enemy.isPlayerDetected() && enemy.isPlayerDetected().distance < enemy.attackDistance)
            stateMachine.ChangeState(enemy.attackState);
        if (Vector2.Distance(player.transform.position, enemy.transform.position) > 7)
            stateMachine.ChangeState(enemy.moveState);
    }
}
