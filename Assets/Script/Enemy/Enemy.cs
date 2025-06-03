using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    #region Components
    [SerializeField] protected LayerMask playerMask;
    [Header("Move info")]
    public float moveSpeed = 0;
    public float idleTime = 0;
    protected float defaultMoveSpeed;
    [Header("Attack info")]
    public float attackDistance = 0;
    [Header("Stun info")]
    public float stunDuration;
    public bool canBeStunned { get; private set; }
    [SerializeField] protected GameObject counterImage;
    #endregion
    #region Listeners
    public virtual RaycastHit2D isPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * (flip ? 1 : -1), 50, playerMask);

    #endregion
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * (flip ? 1 : -1), transform.position.y, transform.position.z));
    }
    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }
    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }
    public virtual void ReceiveCounterAttack() { }

    public virtual void FreezeTime(bool _timeFrozen)
    {
        if (_timeFrozen)
        {
            moveSpeed = 0;
            anim.speed = 0;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            anim.speed = 1;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        defaultMoveSpeed = moveSpeed;
    }

    public override void Flip()
    {
        base.Flip();
    }

    public override void FlipController(float _x)
    {
        base.FlipController(_x);
    }

    protected override IEnumerator KnockBack(Entity attacker)
    {
        return base.KnockBack(attacker);
    }
}
