using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (player.isGrounded())
            rb.linearVelocity = new Vector2(rb.linearVelocityX, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (rb.linearVelocityY == 0 && player.isGrounded())
            stateMachine.ChangeState(player.idleState);
        if(player.isWalled())
            stateMachine.ChangeState(player.wallState);
    }
}
