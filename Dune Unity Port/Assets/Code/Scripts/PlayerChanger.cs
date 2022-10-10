using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChanger : MonoBehaviour
{
    public AbilitySystem player1; 
    public AbilitySystem player2; 
    public AbilitySystem player3; 
    
    // Start is called before the first frame update
    void Start()
    {
        player1.isPlayerSelected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            player1.isPlayerSelected = true;
            player2.isPlayerSelected = false;
            player3.isPlayerSelected =  false;
            Debug.Log(player1.isPlayerSelected.ToString() + " " + player2.isPlayerSelected.ToString() + " " + player3.isPlayerSelected.ToString());
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            player1.isPlayerSelected = false;
            player2.isPlayerSelected = true;
            player3.isPlayerSelected =  false;
            Debug.Log(player1.isPlayerSelected.ToString() + " " + player2.isPlayerSelected.ToString() + " " + player3.isPlayerSelected.ToString());
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            player1.isPlayerSelected = false;
            player2.isPlayerSelected = false;
            player3.isPlayerSelected =  true;
            Debug.Log(player1.isPlayerSelected.ToString() + " " + player2.isPlayerSelected.ToString() + " " + player3.isPlayerSelected.ToString());
        }
    }
}
