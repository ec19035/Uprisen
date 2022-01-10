using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public float playerHealth = 50.0f;
    public float playerMana = 10.0f;
    public int healthPotions = 0;
    public int strengthPotions = 0;
    public int manaPotions = 0;

    void Awake(){
        if (instance != null){
            Debug.Log("zFix Singleton");
        }
        instance = this;
    }

    public float IncreasePlayerHealth(float amount){
        playerHealth += amount;
        return playerHealth;
    }

    public float DecreasePlayerHealth(float amount){
        playerHealth -= amount;
        return playerHealth;
    }

    public float IncreasePlayerMana(float amount){
        playerMana += amount;
        return playerMana;
    }

    public float DecreasePlayerMana(float amount){
        playerMana -= amount;
        return playerMana;
    }

    public int DecreaseHPotion(){
        healthPotions -= 1;
        return healthPotions;
    }

    public int IncreaseHPotion(){
        healthPotions += 1;
        return healthPotions;
    }

    public int DecreaseSPotion(){
        strengthPotions -= 1;
        return strengthPotions;
    }

    public int IncreaseSPotion(){
        strengthPotions += 1;
        return strengthPotions;
    }

    public int DecreaseMPotion(){
        manaPotions -= 1;
        return manaPotions;
    }

    public int IncreaseMPotion(){
        manaPotions += 1;
        return manaPotions;
    }
   
}
