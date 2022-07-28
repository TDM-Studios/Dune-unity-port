using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    BASIC,
    TANK,
    UNDISTRACTABLE,
    AIR
}

public enum EnemyState
{
    IDLE,
    PATROLING,
    CAUTIOUS,
    SEARCHING,
    SHOOTING,
    STUNNED,
    DISTRACTED,
    DEATH
}

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public EnemyType type;
    public Mesh enemyMesh;
    public float agentSpeed;
    public float agentSpeedRun;
    public float coneRadius;
    [Range(0, 360)]
    public float coneAngle;
    public GameObject bulletPrefab;
    public float shootCooldown;
    public float detectionTime;
    public float maxDetectionTime;
}
