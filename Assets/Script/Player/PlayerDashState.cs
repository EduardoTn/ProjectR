using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(StateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
        player.rb.excludeLayers = player.enemyMask;
        if (player.skill.clone.CanUseSkill() && player.isEnemyinArea())
            player.skill.clone.SkillCast(player.transform.position, player.flip);
    }

    public override void Exit()
    {
        base.Exit();
        ignoreInput = false;
        player.rb.excludeLayers = LayerMask.GetMask("Nothing");
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
