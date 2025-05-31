using UnityEngine;

public class Enemy : Entity
{
    #region Components
    [SerializeField] protected LayerMask playerMask;
    [Header("Move info")]
    public float moveSpeed = 0;
    public float idleTime = 0;
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
}
