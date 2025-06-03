using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    [Header("Knockback info")]
    [SerializeField] protected float knockbackDuration;
    public Vector2 knockbackDirection;
    public bool isKnocked;
    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected LayerMask groundMask;
    [SerializeField] protected float wallDistance = 0.1f;
    [SerializeField] protected float groundDistance = 0.15f;
    public Transform attackCheck;
    public float attackCheckRadius;
    public Rigidbody2D rb;
    [Header("Animation info")]
    protected SpriteRenderer sr;
    public EntityFX fx { get; private set; }
    public Animator anim;
    public bool flip = true;
    public StateMachine stateMachine { get; private set; }
    #endregion
    #region Listeners
    public virtual bool isGrounded() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, groundMask);
    public virtual bool isWalled() => Physics2D.Raycast(wallCheck.position, Vector2.right, wallDistance, groundMask);
    public virtual void animationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    #endregion
    protected virtual void Start()
    {
        fx = GetComponent<EntityFX>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Awake()
    {
        stateMachine = new StateMachine();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        stateMachine.currentState.Update();
    }
    public virtual void Damage(bool isHeavy, Entity attacker)
    {
        fx.StartCoroutine("FlashFX");
        if (isHeavy)
            StartCoroutine(KnockBack(attacker));
    }
    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked)
            return;
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    public virtual void Flip()
    {
        flip = !flip;
        transform.Rotate(0f, 180f, 0f);
    }
    public virtual void FlipController(float _x)
    {
        if (rb.linearVelocityX > 0 && !flip)
        {
            Flip();
        }
        else if (rb.linearVelocityX < 0 && flip)
        {
            Flip();
        }
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    protected virtual IEnumerator KnockBack(Entity attacker)
    {
        isKnocked = true;
        rb.linearVelocity = new Vector2(knockbackDirection.x * (attacker.flip ? 1 : -1), knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }

    public void MakeTransparent(bool _transparent)
    {
        if (_transparent)
        {
            sr.color = Color.clear;
        }
        else
        {
            sr.color = Color.white;
        }
    }
}
