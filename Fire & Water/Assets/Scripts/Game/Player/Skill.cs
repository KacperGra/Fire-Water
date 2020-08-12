using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Skill")]
public class Skill : ScriptableObject
{
    public float manaCost;
    public float cooldown;
    public float currentCooldown;
    public bool IsReady;

    public Skill(float _manaCost, float _cooldown)
    {
        manaCost = _manaCost;
        cooldown = _cooldown;
        IsReady = true;
        currentCooldown = 0f;
    }
}
