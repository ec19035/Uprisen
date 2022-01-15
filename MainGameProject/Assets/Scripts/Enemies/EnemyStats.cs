using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Applied To: Game Manager
// Purpose: Keep track of enemy stats, calculates enemy parameters for difficulty

public class EnemyStats : MonoBehaviour
{
    // variables to keep track of game state
    public static EnemyStats instance;
    public int bosses;
    public GameObject[] Gates;
    public string playMode;

    // makes EnemyStats instance, called in awake as it is called in other start methods
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

    // calculates health based on game mode
    public float GetHealth(GameObject enemy){
        float difficultyBoost = 0.0f;
        if (playMode == "Medium"){
            difficultyBoost = 50.0f;
        } else if (playMode == "Hard"){
            difficultyBoost = 100.0f;
        }

        if (enemy.tag == "Turret" || enemy.tag == "Eye"){
            return 50.0f + difficultyBoost;
        } else if (enemy.tag == "Boss"){
            return 100.0f + difficultyBoost;
        }
        return 10.0f;
    }

    // calculates enemy strength based on game mode
    public float GetStrength(GameObject enemy){
        float difficultyBoost = 0.0f;
        if (playMode == "Medium"){
            difficultyBoost = 2.5f;
        } else if (playMode == "Hard"){
            difficultyBoost = 5.0f;
        }

        if (enemy.tag == "Turret" || enemy.tag == "Eye"){
            return 5.0f + difficultyBoost;
        } else if (enemy.tag == "Boss"){
            return 10.0f + difficultyBoost;
        }
        return 10.0f;
    }

    // counts how many bosses have been destroyed
    // unlocks gates to other areas accordingly
    public int increaseBosses(){
        bosses += 1;
        for (int i = 0; i < bosses; i++){
            Gates[i].gameObject.transform.position += new Vector3(0,5.0f,0);
        }
        return bosses;
    }

    // used for loading saved data 
    public void SetBosses(int num){
        bosses = num;
    }
}
