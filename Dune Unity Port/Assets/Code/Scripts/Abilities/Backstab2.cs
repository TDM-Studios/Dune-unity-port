using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backstab2 : MonoBehaviour
{
    GameObject owner;
    GameObject destination;
    public AbilitySystem player2;
    public float amount = 1f;

    void Awake()
    {
        player2 = GameObject.Find("Player 2").GetComponent<AbilitySystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        owner = player2.gameObject;
        destination = player2.hit.transform.gameObject;

        owner.transform.forward = destination.transform.forward;
        owner.transform.position = destination.transform.position - destination.transform.forward * amount; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
