using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){  
        
    }

    void OnTriggerEnter (Collider other){
        if (other.tag == "item"){
            Destroy(other.gameObject);
            Debug.Log("destroy item");
        }
    }
}
