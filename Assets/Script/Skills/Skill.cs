using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;


    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill() => (cooldownTimer < 0);

    public virtual void UseSkill()
    {
        cooldownTimer = cooldown;
    }
}
