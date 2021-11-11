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
    // Start is called before the first frame update
    void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = true;
        agent.destination = points[location].transform.position;
        location = 0;
    }

    // Update is called once per frame
    void Update(){
        if(Vector3.Distance(this.transform.position, points[location].transform.position) <= 2f){
            Iterate();
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
