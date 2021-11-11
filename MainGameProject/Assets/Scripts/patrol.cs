using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class patrol : MonoBehaviour
{
    [SerializeField]
    private GameObject[] points;
    [SerializeField]
    private NavMeshAgent agent;
    private int location;
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = true;
        agent.destination = points[location].transform.position;
        location = 0;
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
