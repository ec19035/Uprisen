using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Applied To: Boundary(Cube)
// Purpose: Destroy any game objects that are not the 
// player that have fallen of the map.

public class Destroy : MonoBehaviour{

    // checks collision then destroys if not player
    void OnCollisionEnter(Collision other){
        if (other.gameObject.tag != "Player"){
            Destroy(other.gameObject);
        }
    }

}
