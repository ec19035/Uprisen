using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Difficulty : MonoBehaviour
{
    public static string difficulty;

    void Awake(){
        DontDestroyOnLoad(gameObject);
    }

    public void Easy(){
        difficulty = "Easy";
        SceneManager.LoadScene("FinalPlatform");
    }

    public void Medium(){
        difficulty = "Medium";
        SceneManager.LoadScene("FinalPlatform");
    }

    public void Hard(){
        difficulty = "Hard";
        SceneManager.LoadScene("FinalPlatform");
    }

    public void LoadGame(){
        difficulty = "LoadGame";
        SceneManager.LoadScene("FinalPlatform");
    }

    public void BackToMainMenu(){
        SceneManager.LoadScene("Menu");
    }

    public static void CutToCredits(){
        SceneManager.LoadScene("Credits");
    }
}
