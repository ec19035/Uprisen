using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Used to reset scene when player falls out of map
// onto boundary.

public class destroy : MonoBehaviour{

    // checks collision
    // general so will work on any scene and reset it if a player interacts with it
    void OnTriggerEnter(Collider other){
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
        //Application.LoadLevel(0);
    }

}
