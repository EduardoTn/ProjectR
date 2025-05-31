using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(StateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }
    public override void Update()
    {
        base.Update();
        if (xInput == 0)
            stateMachine.ChangeState(player.idleState);
    }
}
