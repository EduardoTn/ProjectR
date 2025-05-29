using UnityEngine;

public class State
{
    protected StateMachine stateMachine;
    protected Entity entity;
    protected string animBoolName;
    public bool ignoreInput = false;
    protected bool triggerCalled;

    public State(StateMachine _stateMachine, Entity _entity, string _animBoolName)
    {
        this.stateMachine = _stateMachine;
        this.entity = _entity;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        entity.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);
    }
    public virtual void Update()
    {

    }
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
