using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Applied To: Canvas
// Purpose: Update UI and game managers on potion counts

public class InventoryUI : MonoBehaviour
{
    // potion counters
    public Text hPotionText;
    private int hPotionCount;
    public Text sPotionText;
    private int sPotionCount;
    public Text mPotionText;
    private int mPotionCount;

    // Start is called before the first frame update
    // Initialises values
    void Start(){
        hPotionCount = PlayerStats.instance.healthPotions;
        hPotionText.text = "x" + hPotionCount;
        sPotionCount = PlayerStats.instance.strengthPotions;
        sPotionText.text = "x" + sPotionCount;
        mPotionCount = PlayerStats.instance.manaPotions;
        mPotionText.text = "x" + mPotionCount;
    }

    // Update is called once per frame
    // Checks which potion the user wants to use and updates UI
    void Update(){  
        if (Input.GetKeyDown("1") && hPotionCount > 0){
            if (PlayerStats.instance.playerHealth != 100){
                PlayerStats.instance.IncreasePlayerHealth(10.0f);
                hPotionCount = PlayerStats.instance.DecreaseHPotion();
                hPotionText.text = "x" + hPotionCount;
            }
        } else if (Input.GetKeyDown("2") && sPotionCount > 0){
            PlayerStats.instance.IncreasePlayerStrength(10.0f);
            sPotionCount = PlayerStats.instance.DecreaseSPotion();
            sPotionText.text = "x" + sPotionCount;
        } else if (Input.GetKeyDown("3") && mPotionCount > 0){
            if (PlayerStats.instance.playerMana != 100){
                PlayerStats.instance.IncreasePlayerMana(10.0f);
                mPotionCount = PlayerStats.instance.DecreaseMPotion();
                mPotionText.text = "x" + mPotionCount;
            }
        }
    }

    // checks if the player has tried to collect any potions
    void OnTriggerEnter(Collider other){
        if (other.tag == "HealthPotion"){ // checks tag
            Destroy(other.gameObject); // removes from scene
            hPotionCount = PlayerStats.instance.IncreaseHPotion(); // update game manager
            hPotionText.text = "x" + hPotionCount; // update UI
        } else if (other.tag == "StrengthPotion"){
            Destroy(other.gameObject);
            sPotionCount = PlayerStats.instance.IncreaseSPotion();
            sPotionText.text = "x" + sPotionCount;
        } else if (other.tag == "ManaPotion"){
            Destroy(other.gameObject);
            mPotionCount = PlayerStats.instance.IncreaseMPotion();
            mPotionText.text = "x" + mPotionCount;
        }
    }
}
