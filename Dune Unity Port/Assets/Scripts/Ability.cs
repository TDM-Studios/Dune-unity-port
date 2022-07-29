using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    PROJECTILE_TO_GROUND,
    PROJECTILE_TO_TARGET,
    MELEE_TO_TARGET,
    AREA_OF_EFFECT,
    CONTROLLABLE
}

public enum Key
 {
    Q,
    W,
    E,
    R
 }

[System.Serializable]
public class Ability 
{
    public string name;
    [HideInInspector]
    public GameObject owner;
    public AbilityType type;
    public GameObject body;
    public Key keyBind;
    //-1 for no charges!
    public int charges;
    public float cooldown;
    [HideInInspector]
    public float counter;
    public bool onCooldown;
    
}
