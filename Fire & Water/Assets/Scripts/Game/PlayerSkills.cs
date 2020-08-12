using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public enum SkillType
    {
        MULTI_SHOOT = 0
    }

    private List<SkillType> unlockedSkillsType;

    public PlayerSkills()
    {
        unlockedSkillsType = new List<SkillType>();
    }

    public void UnlockSkill(SkillType _skillType)
    {
        unlockedSkillsType.Add(_skillType);
    }

    public bool IsSkillUnlocked(SkillType _skillType)
    {
        return unlockedSkillsType.Contains(_skillType);
    }

    public int GetSkillIndex(SkillType _skillType)
    {
        return (int)_skillType;
    }
}
