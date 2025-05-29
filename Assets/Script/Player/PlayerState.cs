using UnityEngine;

public class PlayerState : State
{
    protected Player player;
    public float xInput;
    protected Rigidbody2D rb;
    protected float stateTimer = 0f;

    public PlayerState(StateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
        this.entity = this.player = _player;
        rb = _player.GetComponent<Rigidbody2D>();
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
        stateTimer -= Time.deltaTime;
        if (stateTimer < 0)
        {
            xInput = Input.GetAxisRaw("Horizontal");
            player.anim.SetFloat("yVelocity", rb.linearVelocityY);
            if (!ignoreInput)
            {
                player.SetVelocity(xInput * player.speed, player.rb.linearVelocityY);
            }
            switch (player.isGrounded())
            {
                case false:
                    if(!player.isWalled() && !ignoreInput)
                        stateMachine.ChangeState(player.jumpState);
                    break;
                case true:
                    if (Input.GetButtonDown("Jump"))
                        stateMachine.ChangeState(player.jumpState);
                    if (Input.GetKey(KeyCode.Mouse0) && !ignoreInput)
                        stateMachine.ChangeState(player.attackState);
                    break;
            }
        }
    }
}
