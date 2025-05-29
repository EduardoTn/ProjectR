using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();
    private void AnimationTrigger()
    {
        player.animationTrigger();
    }
}
