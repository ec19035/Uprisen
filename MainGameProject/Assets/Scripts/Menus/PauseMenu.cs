using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Applied To: Canvas
// Purpose: Display pause menu 

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseUI;

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.P)){
            if (gamePaused){
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume(){
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        gamePaused = false;
    }

    void Pause(){
        pauseUI.SetActive(true);
        Time.timeScale = 0.0f;
        gamePaused = true;
    }

    public void QuitGame(){
        SceneManager.LoadScene("Menu");
    }
}
