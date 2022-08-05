using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // CompareTag doesn't allocate memory, != does
        {
            // TODO: Should level manager handle this from its script, and call SavePlayer and SaveEnemy?
            SaveSystem.SavePlayer(player);
            Debug.Log("Checkpoint");
        }
    }
}
