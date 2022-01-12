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
        difficulty = "easy";
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
}
