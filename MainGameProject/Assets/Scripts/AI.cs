using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent; // Used to model AI movement
    [SerializeField]
    private GameObject player; // ges refernece of player to claculate distance from AI
    private float health;
    [SerializeField]
    private Slider displayHealth;
    
    // Start is called before the first frame update
    void Start(){
        agent = GetComponent<NavMeshAgent>(); // gets nav mesh agent
        agent.autoBraking = true; // slows down near nodes   
        health = 100.0f;
        displayHealth.value = 1.0f;
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "Melee"){
            health = health - 5.0f;
            if(health <= 0.0f){
                Destroy(this.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update(){
        displayHealth.value = health/100.0f;
        if(Vector3.Distance(this.transform.position, player.transform.position) <= 10f){
            agent.destination = player.transform.position + new Vector3(2,2,2);
            transform.LookAt(player.transform.position);
        }
        
    }
}
