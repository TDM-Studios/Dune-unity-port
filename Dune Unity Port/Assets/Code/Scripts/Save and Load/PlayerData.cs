using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health;
    public int maxHealth;
    public float[] position;
    public float[] rotation;

    // TODO: Save habilities


    public PlayerData(PlayerController player)
    {
        health = player.health;
        maxHealth = player.maxHealth;

        Vector3 p = player.transform.position;
        position = new float[3];
        position[0] = p.x;
        position[1] = p.y;
        position[2] = p.z;

        Quaternion q = player.transform.rotation;
        rotation = new float[4];
        rotation[0] = q.x;
        rotation[1] = q.y;
        rotation[2] = q.z;
        rotation[3] = q.w;
    }

}
