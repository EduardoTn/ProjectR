using UnityEngine;

public class Skelleton : Entity
{
    public SkelletonIdleState idleState { get; private set; }
    public SkelletonMoveState moveState { get; private set; }

    public float moveSpeed = 0;
    public float idleTime = 0;
    protected override void Awake()
    {
        base.Awake();
        idleState = new SkelletonIdleState(stateMachine, this, "Idle");
        moveState = new SkelletonMoveState(stateMachine, this, "Move");
    }
    protected override void Start()
    {
        base.Start();
        base.stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    protected override void Update()
    { 
        base.Update();
    }
}
