using UnityEngine;

public class PlayerHitState : PlayerState
{
    public PlayerHitState(StateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
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
        if (!player.isKnocked)
            stateMachine.ChangeState(player.idleState);
    }
}
