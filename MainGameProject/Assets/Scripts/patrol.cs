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
    private GameObject[] points;
    [SerializeField]
    private NavMeshAgent agent;
    private int location;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject door;
    // Start is called before the first frame update
    void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = true;
        agent.destination = points[location].transform.position;
        location = 0;
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "Melee"){
            Destroy(gameObject);
            Destroy(door);
        }
    }

    // Update is called once per frame
    void Update(){
        if(Vector3.Distance(this.transform.position, player.transform.position) <= 10f){
            agent.destination = player.transform.position;
        }
        if(Vector3.Distance(this.transform.position, points[location].transform.position) <= 2f){
                Iterate();
        }
        if(Vector3.Distance(this.transform.position, player.transform.position) <= 1f){
            Debug.Log("attack");
        }
    }

    void Iterate(){
        if (location < points.Length-1){
            location += 1;
        } else {
            location = 0; 
        }
        agent.destination = points[location].transform.position;
    }
}
