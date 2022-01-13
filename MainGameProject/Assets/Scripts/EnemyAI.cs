using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent; // Used to model AI movement

    private float health;

    private GameObject playerObj;
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.tag != "Turret"){
            agent = GetComponent<NavMeshAgent>(); // gets nav mesh agent
        } 
        health = EnemyStats.instance.GetHealth(this.gameObject);
        playerObj = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag != "Turret"){
            Move();
        } 
    }

    void Move(){
        //Positions AI in front of player
        //With unity world axis, AI will go to different sides of the player - creating unpredictable movement
        agent.destination = playerObj.transform.position + new Vector3(0,2,2); 
    }

    void OnCollisionEnter(Collision other){
        if(this.gameObject.tag == "Boss" && other.gameObject.tag == "Melee"){
            Destroy(this.gameObject);
            int checkFinished = EnemyStats.instance.increaseBosses();
            if (checkFinished == 1){
                Difficulty.CutToCredits();
            }
        }
    }

}
