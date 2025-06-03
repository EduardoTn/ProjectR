using UnityEngine;

public class PlayerBlackHoleState : PlayerState
{
    private float flyTime = .4f;
    private bool skillUsed;
    private float defaulGratity;
    public PlayerBlackHoleState(StateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        skillUsed = false;
        stateTimer = flyTime;
        defaulGratity = player.rb.gravityScale;
        player.rb.gravityScale = 0;
        ignoreInput = true;
    }

    public override void Exit()
    {
        base.Exit();
        ignoreInput = false;
        player.rb.gravityScale = defaulGratity;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
            player.rb.linearVelocity = new Vector2(0, 14);

        if (stateTimer <= 0)
        {
            player.rb.linearVelocity = new Vector2(0, -.1f);
            if (!skillUsed)
            {
                player.skill.blackHole.UseSkill();
                skillUsed = true;
            }
        }
    }
}
