using UnityEngine;

public class SkelletonIdleState : SkelletonBaseState
{
    public SkelletonIdleState(StateMachine _stateMachine, Skelleton _entity, string _animBoolName) : base(_stateMachine, _entity, _animBoolName)
    {
    }
    public override void Enter()
    {
        enemy.SetVelocity(0, 0);
        base.Enter();
        stateTimer = enemy.idleTime;
    }
    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            enemy.stateMachine.ChangeState(enemy.moveState);
    }
}