using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Text hPotionText;
    private int hPotionCount;
    public Text sPotionText;
    private int sPotionCount;
    public Text mPotionText;
    private int mPotionCount;

    // Start is called before the first frame update
    void Start(){
        hPotionCount = PlayerStats.instance.healthPotions;
        hPotionText.text = "x" + hPotionCount;
        sPotionCount = PlayerStats.instance.strengthPotions;
        sPotionText.text = "x" + sPotionCount;
        mPotionCount = PlayerStats.instance.manaPotions;
        mPotionText.text = "x" + mPotionCount;
    }

    // Update is called once per frame
    void Update(){  
        if (Input.GetKeyDown("1") && hPotionCount > 0){
            if (PlayerStats.instance.playerHealth != 100){
                PlayerStats.instance.IncreasePlayerHealth(10.0f);
                hPotionCount = PlayerStats.instance.DecreaseHPotion();
                hPotionText.text = "x" + hPotionCount;
            }
        } else if (Input.GetKeyDown("2") && sPotionCount > 0){
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

    void OnTriggerEnter(Collider other){
        if (other.tag == "HealthPotion"){
            Destroy(other.gameObject);
            hPotionCount = PlayerStats.instance.IncreaseHPotion();;
            hPotionText.text = "x" + hPotionCount;
        } else if (other.tag == "StrengthPotion"){
            Destroy(other.gameObject);
            sPotionCount = PlayerStats.instance.IncreaseSPotion();;
            sPotionText.text = "x" + sPotionCount;
        } else if (other.tag == "ManaPotion"){
            Destroy(other.gameObject);
            mPotionCount = PlayerStats.instance.IncreaseMPotion();;
            mPotionText.text = "x" + mPotionCount;
        }
    }
}
