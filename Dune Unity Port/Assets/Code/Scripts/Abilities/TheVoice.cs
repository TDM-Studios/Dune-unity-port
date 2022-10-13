using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheVoice : MonoBehaviour
{
    public PlayerChanger changer;
    private void Start()
    {
        changer = GameObject.Find("AbilitySystemHelper").GetComponent<PlayerChanger>();
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            changer.SetPlayer(2);
            Debug.Log(hit.transform.name);
            if(hit.transform.tag == "Enemy")
                hit.transform.GetComponent<EnemyContoller>().controlled = true;
            else
            {
                //resetear cooldowns
            }
        }
        Destroy(gameObject);
    }
}
