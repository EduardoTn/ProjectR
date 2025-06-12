using Unity.VisualScripting;
using UnityEngine;

public class SkelletoonDieState : SkelletonBaseState
{
    public SkelletoonDieState(StateMachine _stateMachine, Skelleton _entity, string _animBoolName) : base(_stateMachine, _entity, _animBoolName)
    {
        this.enemy = _entity;
    }
    public override void Enter()
    {
        base.Enter();
        enemy.CloseCounterAttackWindow();
        enemy.rb.gravityScale = 0;
        enemy.rb.mass = 0;
        enemy.SetVelocity(0,0);
        enemy.GetComponent<Collider2D>().enabled = false;
        GameObject.Destroy(enemy.gameObject, 10f);
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
