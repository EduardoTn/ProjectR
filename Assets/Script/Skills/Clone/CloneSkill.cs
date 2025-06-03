using UnityEngine;

public class CloneSkill : Skill
{
    [SerializeField] private GameObject clone;
    private CloneSkillController controller;
    public float cloneDuration;
    public float colorLoosingSpeed;
    public void SkillCast(Vector3 _position, bool _flip)
    {
        base.UseSkill();
        controller = new CloneSkillController();
        controller.CreateClone(_position, _flip, clone);
    }
}
