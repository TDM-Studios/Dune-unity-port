using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backstab : MonoBehaviour
{
    GameObject owner;
    GameObject destination;
    public AbilitySystem player1;
    public float amount = 1f;

    void Awake()
    {
        player1 = GameObject.Find("Player 1").GetComponent<AbilitySystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        owner = player1.gameObject;
        destination = player1.hit.transform.gameObject;

        owner.transform.forward = destination.transform.forward;
        owner.transform.position = destination.transform.position - destination.transform.forward * amount; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
