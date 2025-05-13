using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected float xInput;
    protected Rigidbody2D rb;
    protected float stateTimer = 0f;
    private string animBoolName;
    public bool ignoreInput = false;

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
        stateTimer -= Time.deltaTime;
        if (stateTimer < 0)
        {
            xInput = Input.GetAxisRaw("Horizontal");
            player.anim.SetFloat("yVelocity", rb.linearVelocityY);
            if(!ignoreInput)
                player.SetVelocity(xInput * player.speed, player.rb.linearVelocityY);
            switch (player.isGrounded())
            {
                case false:
                    if(!player.isWalled() && !ignoreInput)
                        stateMachine.ChangeState(player.jumpState);
                    break;
                case true:
                    if (Input.GetButtonDown("Jump"))
                        stateMachine.ChangeState(player.jumpState);
                    break;
            }
        }
    }
}
