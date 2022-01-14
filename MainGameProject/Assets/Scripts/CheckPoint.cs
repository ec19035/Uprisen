using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    CharacterController body;
    public Vector3 lastCheckPoint;

    // Start is called before the first frame update
    void Start(){
        body = GetComponent<CharacterController>();
        lastCheckPoint = body.transform.position;
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "CheckPoint"){
            lastCheckPoint = transform.position;
        } else if (other.tag == "Boundary"){
            body.enabled = false;
            body.transform.position = lastCheckPoint;
            body.enabled = true;
        }
    }
}
