using UnityEngine;

public class CloneSkill : Skill
{
    [SerializeField] private GameObject clone;
    public float cloneDuration;
    public float colorLoosingSpeed;
    public override void UseSkill()
    {
        base.UseSkill();
        Instantiate(clone, PlayerManager.instance.player.transform.position, Quaternion.identity);
    }
}
