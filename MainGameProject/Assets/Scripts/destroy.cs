using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class destroy : MonoBehaviour{
    void OnTriggerEnter(Collider other){
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        //Application.LoadLevel(0);
    }
}
