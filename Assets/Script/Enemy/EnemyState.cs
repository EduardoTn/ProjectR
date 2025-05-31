using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyState : State
{
    protected Rigidbody2D rb;
    protected float stateTimer = 0f;
    protected Transform player;
    public EnemyState(StateMachine _stateMachine, Entity _entity, string _animBoolName) : base(_stateMachine, _entity, _animBoolName)
    {
    }

    public override void Enter()
    {
        player = PlayerManager.instance.player.transform;
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
    }
}
