using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    GameObject owner;
    GameObject destination;
    public AbilitySystem player1;
    Rigidbody rb;
    [SerializeField]
    bool canPickUpKnife = false;

    void Awake()
    {
        player1 = GameObject.Find("Player 1").GetComponent<AbilitySystem>();
        rb = gameObject.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        canPickUpKnife = false;
        owner = player1.gameObject;
        destination = player1.hit.transform.gameObject;

        this.transform.LookAt(destination.transform, Vector3.up);

        Vector3 dir = (destination.transform.position - owner.transform.position);
        dir.y += 0.5f;
        dir = dir.normalized;
        
        rb.velocity = dir * player1.abilityList[1].range;
    }

    // Update is called once per frame
    void Update()
    {
        if(!canPickUpKnife && (this.transform.position - owner.transform.position).magnitude > 2f)
        {
            canPickUpKnife = true;
        }
        else if(canPickUpKnife && (this.transform.position - owner.transform.position).magnitude < 1f)
        {
            player1.abilityList[0].charges++;
            GameObject.Destroy(gameObject);
        }
    }
}
