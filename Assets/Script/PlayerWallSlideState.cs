using System.Collections;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
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

        if (!ignoreInput)
        {
            if (Input.GetButtonDown("Jump"))
                player.wallJump();
            if (xInput != 0)
                player.rb.linearVelocityY = player.rb.linearVelocityY * 0.7f;
        }

        if (rb.linearVelocityY == 0 && player.isGrounded())
            stateMachine.ChangeState(player.idleState);
    }
}
