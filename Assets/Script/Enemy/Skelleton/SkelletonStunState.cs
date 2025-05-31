using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkelletonStunState : SkelletonBaseState
{
    public SkelletonStunState(StateMachine _stateMachine, Skelleton _entity, string _animBoolName) : base(_stateMachine, _entity, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.stunDuration;
        enemy.rb.linearVelocity = new Vector2(enemy.knockbackDirection.x * (enemy.flip ? -1 : 1), enemy.knockbackDirection.y);
        enemy.fx.InvokeRepeating("RedColorBlink", 0, 0.1f);
        enemy.CloseCounterAttackWindow();
    }
    public override void Exit()
    {
        base.Exit();
        enemy.fx.CancelInvokes();
    }
    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.idleState);
    }
}
