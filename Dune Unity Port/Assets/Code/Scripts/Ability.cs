using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    PROJECTILE_TO_GROUND,
    PROJECTILE_TO_TARGET_ENEMY,
    PROJECTILE_TO_TARGET_ALLY,
    MELEE_TO_TARGET,
    AREA_OF_EFFECT,
    CONTROLLABLE
}

[System.Serializable]
public class Ability 
{
    public string name;
    public GameObject owner;
    public AbilityType type;
    public GameObject prefab;
    //-1 for no charges!
    public int charges;
    public int range;
    public float cooldown;
    [HideInInspector]
    public float counter;
    public bool onCooldown;
}
