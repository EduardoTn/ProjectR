using UnityEngine;

public class BlackHoleSkill : Skill
{
    [SerializeField] private GameObject blackHole;
    [Space]
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float cloneAttackCooldown;
    [SerializeField] private int amountOfAttacks;
    [SerializeField] private float blackHoleDuration;
    private BlackHoleSkillController blackHoleController;
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        if (cooldownTimer < 0)
        {
            base.UseSkill();
            GameObject newBlackHole = Instantiate(blackHole, PlayerManager.instance.player.transform.position, Quaternion.identity);
            blackHoleController = newBlackHole.GetComponent<BlackHoleSkillController>();
            blackHoleController.SetupBlackHole(maxSize, growSpeed, amountOfAttacks, cloneAttackCooldown, blackHoleDuration);
        }

    }

    protected override void Update()
    {
        base.Update();
    }
}
