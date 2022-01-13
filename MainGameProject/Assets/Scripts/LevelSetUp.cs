using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetUp : MonoBehaviour
{
    public string levelChoice;
    public GameObject[] eyeSpawnPoint;
    public GameObject eyeEnemy;
    public GameObject[] turretSpawnPoint;
    public GameObject turretEnemy;
    public GameObject[] bossSpawnPoint;
    public GameObject[] FireBoss;
    // Start is called before the first frame update
    void Start()
    {
        levelChoice = PlayerStats.instance.choice;
        for (int i = 0; i < eyeSpawnPoint.Length; i++){
            var enem = Instantiate(eyeEnemy, eyeSpawnPoint[i].transform.position, eyeSpawnPoint[i].transform.rotation);
            enem.AddComponent<EnemyAI>();
        }
        for (int i = 0; i < turretSpawnPoint.Length; i++){
            var enemT = Instantiate(turretEnemy, turretSpawnPoint[i].transform.position, turretSpawnPoint[i].transform.rotation);
            enemT.AddComponent<EnemyAI>();
        }
        int numOfBossesBeaten = EnemyStats.instance.bosses;
        for (int i = 0; i < bossSpawnPoint.Length; i++){
            if (i >= numOfBossesBeaten){
                var boss = Instantiate(FireBoss[i], bossSpawnPoint[i].transform.position, bossSpawnPoint[i].transform.rotation);
                boss.AddComponent<EnemyAI>();
            }
        }
    }

}
