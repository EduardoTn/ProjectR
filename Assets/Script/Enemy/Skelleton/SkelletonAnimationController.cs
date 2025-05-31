using UnityEngine;

public class SkelletonAnimationController : MonoBehaviour
{
    private Skelleton skelleton => GetComponentInParent<Skelleton>();
    private void AnimationTrigger()
    {
        skelleton.animationTrigger();
    }
    private void AttackLightTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(skelleton.attackCheck.position, skelleton.attackCheckRadius);
        foreach (var hit in colliders)
        {
            var player = hit.GetComponent<Player>();
            if (player != null)
                player.Damage(false, skelleton);
        }
    }
    private void AttackHeavyTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(skelleton.attackCheck.position, skelleton.attackCheckRadius);
        foreach (var hit in colliders)
        {
            var player = hit.GetComponent<Player>();
            if (player != null)
                player.Damage(true, skelleton);
        }
    }
    protected void OpenCounterWindow() => skelleton.OpenCounterAttackWindow();
    protected void CloseCounterWindow() => skelleton.CloseCounterAttackWindow();
}
