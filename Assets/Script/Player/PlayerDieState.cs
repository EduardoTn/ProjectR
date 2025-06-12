using UnityEngine;

public class PlayerDieState : PlayerState
{
    public PlayerDieState(StateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.rb.gravityScale = 0;
        player.rb.mass = 0;
        player.SetVelocity(0, 0);
        player.GetComponent<Collider2D>().enabled = false;
        GameObject.Destroy(player.gameObject, 10f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        return;
    }
}
