using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetUp : MonoBehaviour
{
    public string levelChoice;
    public GameObject[] spawnPoint;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        levelChoice = PlayerStats.instance.choice;
        for (int i = 0; i < spawnPoint.Length; i++){
            var enem = Instantiate(enemy, spawnPoint[i].transform.position, spawnPoint[i].transform.rotation);
            enem.AddComponent<EnemyAI>();
        }
    }

}
