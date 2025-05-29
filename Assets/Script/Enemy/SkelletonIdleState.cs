using UnityEngine;

public class SkelletonIdleState : EnemyState
{
    private Skelleton enemy;
    public SkelletonIdleState(StateMachine _stateMachine, Entity _entity, string _animBoolName) : base(_stateMachine, _entity, _animBoolName)
    {
        enemy = _entity as Skelleton;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            enemy.stateMachine.ChangeState(enemy.moveState);
    }
}
