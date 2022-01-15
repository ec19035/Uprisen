using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// Applied To: Game manager
// Purpose: Keep track of all player variables, Save the current situation, 
// and load saved game

// Serializeable class 
[System.Serializable]
class PlayerData
{  
    public string lvlChoice;

    public float health;
    public float mana;
    public float strength;
    public int hPotions;
    public int sPotions;
    public int mPotions;

    public int bosses;
    
}


public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public float playerHealth = 100.0f;
    public float playerMana = 50.0f;
    public float playerStrength = 10.0f;
    public int healthPotions = 10;
    public int strengthPotions = 10;
    public int manaPotions = 10;

    public int initBosses;

    public string choice;

    void Awake(){
        if (instance != null){
            Destroy(gameObject);
        }
        instance = this;
        choice = Difficulty.difficulty;
        if (choice == "LoadGame"){
            Load();
        } 
    }

    void Start(){
        if (choice != "Tutorial"){
            EnemyStats.instance.SetBosses(initBosses);
        }
    }

    void OnApplicationQuit()
    {
        if (choice != "Tutorial"){
            Save();
        }
    }

    #region Getters and Setters
    public float IncreasePlayerHealth(float amount){
        playerHealth += amount;
        return playerHealth;
    }

    public float DecreasePlayerHealth(float amount){
        playerHealth -= amount;
        return playerHealth;
    }

    public float IncreasePlayerStrength(float amount){
        playerStrength += amount;
        return playerStrength;
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

    #endregion

    public void Save()
    {
        string filename = Application.persistentDataPath + "/playInfo.dat";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filename ,FileMode.OpenOrCreate);

        PlayerData pd = new PlayerData();
        pd.lvlChoice = choice;
        pd.health = playerHealth;
        pd.strength = playerStrength;
        pd.mana = playerMana;
        pd.hPotions = healthPotions;
        pd.sPotions = strengthPotions;
        pd.mPotions = manaPotions;
        pd.bosses = EnemyStats.instance.bosses;
        
        
        bf.Serialize(file ,pd);
        file.Close();
    }

    public void Load()
    {
        string filename = Application.persistentDataPath + "/playInfo.dat" ;
        if (File.Exists(filename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filename ,FileMode.Open);
            PlayerData pd = (PlayerData) bf.Deserialize(file);
            file.Close();
            
            choice = pd.lvlChoice;
            playerHealth = pd.health;
            playerMana = pd.mana;
            playerStrength = pd.strength;
            healthPotions = pd.hPotions;
            strengthPotions = pd.sPotions;
            manaPotions = pd.mPotions;
            initBosses = pd.bosses;
        }
    }
   
}
