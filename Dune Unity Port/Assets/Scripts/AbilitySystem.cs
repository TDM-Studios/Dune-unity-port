using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    public Ability[] abilityList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Ability a in abilityList)
        {
            if(a.onCooldown)
            {
                a.counter += Time.deltaTime;
                if(a.counter >= a.cooldown)
                {
                    a.onCooldown = false;
                    a.counter = 0f;
                }
            }
        }
    }
}
