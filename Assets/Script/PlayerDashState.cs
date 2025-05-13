using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.dashSpeed * (player.flip ? 1 : -1), 0);
        if (stateTimer <= 0)
        {
            stateMachine.ChangeState(player.idleState);
            if (player.isWalled())
                stateMachine.ChangeState(player.wallState);
        }
    }
}
