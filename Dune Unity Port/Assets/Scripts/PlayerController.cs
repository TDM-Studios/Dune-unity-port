using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;

    PlayerData data;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            health -= 5;
            Debug.Log("Health: " + health);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SavePlayer();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            LoadPlayer();
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        data = SaveSystem.LoadPlayer();
        health = data.health;
        maxHealth = data.maxHealth;

        Vector3 pos = new Vector3();
        pos.x = data.position[0];
        pos.y = data.position[1];
        pos.z = data.position[2];
        transform.position = pos;

        Quaternion rot = new Quaternion();
        rot.x = data.rotation[0];
        rot.y = data.rotation[1];
        rot.z = data.rotation[2];
        rot.w = data.rotation[3];
        transform.rotation = rot;
    }

}
