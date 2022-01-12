using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
class PlayerData
{
    public string lvlChoice;
}


public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public float playerHealth = 50.0f;
    public float playerMana = 10.0f;
    public int healthPotions = 0;
    public int strengthPotions = 0;
    public int manaPotions = 0;

    public string choice;

    void Awake(){
        if (instance != null){
            Destroy(gameObject);
        }
        instance = this;
        Load();
        if (choice == "" || choice == null){
            choice = Difficulty.difficulty;
        }   
        Debug.Log(choice);
    }

    void OnApplicationQuit()
    {
        Save();
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
        
        bf.Serialize(file , pd);
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
        }
    }
   
}
