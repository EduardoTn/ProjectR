using Unity.VisualScripting;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    [SerializeField] Transform attackCheck;
    [SerializeField] float attackCheckRadius = 1;
    [SerializeField] GameObject clone;
    private float cloneDuration;
    private float colorLoosingSpeed;
    private SpriteRenderer sr;
    private float cloneTimer;
    private Animator anim;
    private Player player;
    public bool flip;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = PlayerManager.instance.player;
        cloneDuration = SkillManager.instance.clone.cloneDuration;
        colorLoosingSpeed = SkillManager.instance.clone.colorLoosingSpeed;
        cloneTimer = cloneDuration;
        anim.SetInteger("AttackNumber", Random.Range(1, 3));
        if (!flip)
            transform.Rotate(0f, 180f, 0f);
    }
    public void CreateClone(Vector3 _position, bool _flip, GameObject clone)
    {
        clone.GetComponent<CloneSkillController>().flip = _flip;
        Instantiate(clone, _position, Quaternion.identity);
    }
    void Update()
    {
        cloneTimer -= Time.deltaTime;
        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLoosingSpeed));
            if (sr.color.a <= 0)
                Destroy(this.gameObject);
        }
    }
    private void AnimationTrigger()
    {
        cloneTimer = -.1f;
    }
    private void AttackLightTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        foreach (var hit in colliders)
        {
            var enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
                enemy.Damage(false, player);
        }
    }
    private void AttackHeavyTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        foreach (var hit in colliders)
        {
            var enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
                enemy.Damage(true, player);
        }
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
}
