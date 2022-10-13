using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camouflage : MonoBehaviour
{
    GameObject owner;
    public AbilitySystem player2;
    public Renderer playerRender;
    public Renderer camoRender;
    Material playerMaterial;

    // Time Counter
    float count = 0f;
    float maxTime = 10f;
    bool camouflaged = false;

    void Awake()
    {
        player2 = GameObject.Find("Player 2").GetComponent<AbilitySystem>();
        playerRender = GameObject.Find("Player 2").GetComponent<Renderer>();
        playerMaterial = playerRender.material;
        camoRender = gameObject.GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRender.material = camoRender.material;
        camouflaged = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(camouflaged)
        {
            count += Time.deltaTime;
            if(count > maxTime)
            {
                camouflaged = false;
                playerRender.material = playerMaterial;
                Destroy(gameObject);
            }
        }
    }
}
