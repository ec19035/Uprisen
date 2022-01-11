using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public TextMeshProUGUI instruction;
    private bool start = true;
    private bool firstCorridor = false;
    private bool itemCollect = false;
    private bool secondCorridor = false;
    private bool jumpInstruction = false;
    private bool fightInstruction = false;

    // Start is called before the first frame update
    void Start(){
        instruction.text = "Welcome to this tutorial. Press WASD or the arrow keys to move around.";
    }

    void OnTriggerEnter(Collider other){
        if (other.name == "FirstCheckPoint"){
            firstCorridor = true; 
        }
        if (other.name == "SecondCheckPoint"){
            jumpInstruction = true;
        }
        if (other.name == "ThirdCheckPoint"){
            fightInstruction = true;
        }
        if (other.tag == "HealthPotion"){
            itemCollect = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && start){
            instruction.text = "Proceed outside the corridor to begin.";
            start = false;
        }
        if (firstCorridor){
            instruction.text = "Pass over that potion there to collect.";
            firstCorridor = false;
        }
        if (itemCollect){
            instruction.text = "You have just picked up a potion that increases health. To use it press 1";
            itemCollect = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && secondCorridor == false){
            instruction.text = "Great now head to the next stage.";
            secondCorridor = true;
        }
        if (jumpInstruction){
            instruction.text = "Press the Space Bar to jump over obstacles and holes. \n Don't worry if you fall, you will automatically respawn at your last checkpoint.";
            jumpInstruction = false;
        } 
        if (fightInstruction){
            instruction.text = "Click to perform a melee attack and press J to shoot.";
            fightInstruction = false;
        }
    }
}
