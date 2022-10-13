using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterSeeker : MonoBehaviour
{
    public NavMeshAgent agent;
    public PlayerChanger changer;
    // Start is called before the first frame update
    void Start()
    {
        changer = GameObject.Find("AbilitySystemHelper").GetComponent<PlayerChanger>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        changer.SetPlayer(2);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitPoint;

        if (Physics.Raycast(ray, out hitPoint))
        {
            agent.SetDestination(hitPoint.point);
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            changer.SetPlayer(0);
            Destroy(gameObject);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            changer.SetPlayer(1);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
        if(collision.transform.tag == "Enemy")
        {
            changer.SetPlayer(1);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
