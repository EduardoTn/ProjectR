using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(StateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (player.isGrounded())
            rb.linearVelocity = new Vector2(rb.linearVelocityX, player.jumpForce);
    }
    public override void Update()
    {
        base.Update();
        if (player.isGrounded())
            stateMachine.ChangeState(player.idleState);
        if(player.isWalled() && rb.linearVelocityY <= 0)
            stateMachine.ChangeState(player.wallState);
    }
}
