using UnityEngine;

public class PlayerCounterState : PlayerState
{
    public PlayerCounterState(StateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfulCounter", false);
    }

    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("SuccessfulCounter", false);
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (var hit in colliders)
        {
            var enemy = hit.GetComponent<Enemy>();
            if (enemy != null && enemy.canBeStunned)
            {
                stateTimer = player.counterAttackDuration;
                player.anim.SetBool("SuccessfulCounter", true);
                enemy.ReceiveCounterAttack();
            }
        }

        if (stateTimer < 0 || triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
