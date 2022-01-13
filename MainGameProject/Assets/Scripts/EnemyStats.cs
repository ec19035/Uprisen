using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public static EnemyStats instance;

    public int bosses;

    public string playMode;

    void Awake(){
        if (instance != null){
            Destroy(gameObject);
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playMode = PlayerStats.instance.choice;
    }

    public float GetHealth(GameObject enemy){
        float difficultyBoost = 0.0f;
        if (playMode == "Medium"){
            difficultyBoost = 50.0f;
        } else if (playMode == "Hard"){
            difficultyBoost = 100.0f;
        }

        if (enemy.tag == "Enemy"){
            return 50.0f + difficultyBoost;
        } else if (enemy.tag == "Boss"){
            return 100.0f + difficultyBoost;
        }
        return 10.0f;
    }

    public float GetStrength(GameObject enemy){
        float difficultyBoost = 0.0f;
        if (playMode == "Medium"){
            difficultyBoost = 2.5f;
        } else if (playMode == "Hard"){
            difficultyBoost = 5.0f;
        }

        if (enemy.tag == "Enemy"){
            return 5.0f + difficultyBoost;
        } else if (enemy.tag == "Boss"){
            return 10.0f + difficultyBoost;
        }
        return 10.0f;
    }

    public int increaseBosses(){
        return bosses+=1;
    }

    public void SetBosses(int num){
        bosses = num;
    }
}
