using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 0.6f;
    public PlayerAttackState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ignoreInput = true;
        player.SetVelocity(0f, 0f);
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;
        player.SetVelocity(player.AttackMovement[comboCounter].x * (player.flip ? 1 : -1), player.AttackMovement[comboCounter].y);
        player.anim.SetInteger("ComboCounter", comboCounter);

    }

    public override void Exit()
    {
        base.Exit();
        ignoreInput = false;
        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
