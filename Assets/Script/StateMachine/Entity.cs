using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components

    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected LayerMask groundMask;
    [SerializeField] protected float wallDistance = 0.1f;
    [SerializeField] protected float groundDistance = 0.15f;
    public Rigidbody2D rb;
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
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
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
    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
    }
    public void Flip()
    {
        flip = !flip;
        transform.Rotate(0f, 180f, 0f);
    }
}
