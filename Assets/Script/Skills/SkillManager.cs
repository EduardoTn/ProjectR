using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public DashSkill dash;
    public ParrySkill parry;
    public CloneSkill clone;
    private void Awake()
    {
        if (instance != null) 
            Destroy(instance);
        else
            instance = this;
    }
    private void Start()
    {
        dash = GetComponent<DashSkill>();
        parry = GetComponent<ParrySkill>();
        clone = GetComponent<CloneSkill>();
    }
}
