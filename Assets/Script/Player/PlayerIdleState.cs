using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(StateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }
    public override void Update()
    {
        base.Update();

        if (xInput != 0 && player.isGrounded())
            stateMachine.ChangeState(player.moveState);
    }
}
