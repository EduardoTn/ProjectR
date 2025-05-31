using UnityEngine;

public class SkelletonAttackState : SkelletonBaseState
{
    public SkelletonAttackState(StateMachine _stateMachine, Skelleton _entity, string _animBoolName) : base(_stateMachine, _entity, _animBoolName)
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
        enemy.SetVelocity(0,0);
        if (triggerCalled)
            stateMachine.ChangeState(enemy.idleState);
    }
}
