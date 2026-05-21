using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    public string skillName;
    public Sprite icon;
    [TextArea] public string description;
    public float duration;

    
    public abstract void UseSkill();
}