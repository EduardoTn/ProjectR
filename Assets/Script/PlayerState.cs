using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected float xInput;
    protected Rigidbody2D rb;

    private string animBoolName;

    public PlayerState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.animBoolName = _animBoolName;
        rb = _player.GetComponent<Rigidbody2D>();
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
    public virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        player.anim.SetFloat("yVelocity", rb.linearVelocityY);
        player.SetVelocity(xInput * player.speed, player.rb.linearVelocityY);
        switch (player.isGrounded())
        {
            case false:
                stateMachine.ChangeState(player.jumpState);
                break;
            case true:
                if (Input.GetButtonDown("Jump"))
                    stateMachine.ChangeState(player.jumpState);
                break;
        }

    }
}
