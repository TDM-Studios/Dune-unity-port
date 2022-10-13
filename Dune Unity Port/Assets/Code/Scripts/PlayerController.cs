using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent player;
    public AbilitySystem abilitySystem;
    private void Start()
    {
        cam = Camera.main;
        abilitySystem = gameObject.GetComponent<AbilitySystem>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && abilitySystem.isPlayerSelected && !abilitySystem.isPlayerCasting)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if(Physics.Raycast(ray, out hitPoint))
            {
                player.SetDestination(hitPoint.point);
            }
        }
    }
}
