using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public List<GameObject> enemiesGO = new List<GameObject>();
    public GameObject basicEnemy;
    public GameObject tankEnemy;
    public GameObject airEnemy;

    private void Start()
    {
        enemiesGO.Clear();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            switch (spawnPoints[i].GetComponent<SpawnPoint>().enemyType)
            {
                case EnemyType.BASIC:
                    enemiesGO.Add(Instantiate(basicEnemy, spawnPoints[i].transform));
                    break;
                case EnemyType.TANK:
                    enemiesGO.Add(Instantiate(tankEnemy, spawnPoints[i].transform));
                    break;
                case EnemyType.AIR:
                    enemiesGO.Add(Instantiate(airEnemy, spawnPoints[i].transform));
                    break;
               default:
                    break;
            }
        
        }
    }
    private void Update()
    {
        for (int i = 0; i < enemiesGO.Count; i++)
        {
            if ((enemiesGO[i].GetComponent<EnemyContoller>().pendingToDelete))
            {
                Debug.Log("SADADASDASDASDSADASDASDASD");
            }
        }
    }
}
