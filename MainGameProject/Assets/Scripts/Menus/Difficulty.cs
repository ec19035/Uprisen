using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Applied To: Canvas elements in menu
// Purpose: Allow the user to pass between scenes, 
// is not destroyed so that the scene type can be passsed to 
// game controllers in other scene.

public class Difficulty : MonoBehaviour
{
    // passed to other scripts
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

    public void Tutorial(){
        difficulty = "Tutorial";
        SceneManager.LoadScene("Tutorial");
    }

    public void BackToMainMenu(){
        SceneManager.LoadScene("Menu");
    }

    // static to be called from another script without instantiating Difficulty
    public static void CutToCredits(){
        SceneManager.LoadScene("Credits");
    }
}
