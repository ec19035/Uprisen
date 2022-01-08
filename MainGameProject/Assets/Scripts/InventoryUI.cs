using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Text potionText;
    private int potionCount;

    // Start is called before the first frame update
    void Start(){
        potionCount = PlayerStats.instance.ran;
        potionText.text = "Potions: " + potionCount;
    }

    // Update is called once per frame
    void Update(){  
        if (Input.GetKeyDown("1") && potionCount > 0){
            Debug.Log("S");
            potionCount = PlayerStats.instance.Decrease();
            potionText.text = "Potions:" + potionCount;
        }        

    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "item"){
            Destroy(other.gameObject);
            potionCount += 1;
            potionText.text = "Potions:" + potionCount;
        }
    }
}
