using UnityEngine;

public class SkelletonBaseState : EnemyState
{
    protected Skelleton enemy;

    public SkelletonBaseState(StateMachine _stateMachine, Skelleton _entity, string _animBoolName) : base(_stateMachine, _entity, _animBoolName)
    {
        enemy = _entity;
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
    }
}
