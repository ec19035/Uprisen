using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Used to create an AI that moves around while the character is out
// of range. Then chases the player once they are within range.
// and attacks the player. 
// Also has collision detection incase the player harms the AI
// and destroys it accordingly

public class patrol : MonoBehaviour
{
    // made fields private to protect them
    // used serializable to access them 
    [SerializeField]
    private GameObject[] points; // used to store the two points where the AI walks between
    [SerializeField]
    private NavMeshAgent agent; // Used to model AI movement
    private int location; // stores which node the AI is at
    [SerializeField]
    private GameObject player; // ges refernece of player to claculate distance from AI
    [SerializeField]
    private GameObject door; // Unlocks(*Destroys) door to boss fights

    // Start is called before the first frame update
    void Start(){
        agent = GetComponent<NavMeshAgent>(); // gets nav mesh agent
        agent.autoBraking = true; // slows down near nodes
        agent.destination = points[location].transform.position; // calculates next node to travel to
        location = 0;
    }

    // checks if the AI has been attacked 
    void OnTriggerEnter(Collider other){
        if (other.tag == "Melee"){
            Destroy(gameObject); // destoys AI
            Destroy(door); // unlocks(*destroys) door
        }
    }

    // Update is called once per frame
    // checks what node or player the AI is closest to

    void Update(){
        if(Vector3.Distance(this.transform.position, player.transform.position) <= 10f){
            agent.destination = player.transform.position;
        }
        if(Vector3.Distance(this.transform.position, points[location].transform.position) <= 2f){
            Iterate();
        }
    }

    // (simple) logic to decide which node to go to
    void Iterate(){
        if (location < points.Length-1){
            location += 1;
        } else {
            location = 0; 
        }
        agent.destination = points[location].transform.position; //move towards node
    }
}
