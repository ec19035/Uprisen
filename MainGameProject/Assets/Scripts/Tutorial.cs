using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Applies To: Player
// Purpose: Used to change text through the tutorial to help the player

public class Tutorial : MonoBehaviour
{
    public TextMeshProUGUI instruction;
    private bool start = true;
    private bool firstCorridor = false;
    private bool itemCollect = false;
    private bool secondCorridor = false;
    private bool jumpInstruction = false;
    private bool fightInstruction = false;
    private bool meleeInstruction = false;

    // Start is called before the first frame update
    void Start(){
        instruction.text = "Welcome to this tutorial. Press WASD or the arrow keys to move around.";
    }

    void OnTriggerEnter(Collider other){
        if (other.name == "FirstCheckPoint"){
            firstCorridor = true; 
        }
        else if (other.name == "SecondCheckPoint"){
            jumpInstruction = true;
        }
        else if (other.name == "ThirdCheckPoint"){
            fightInstruction = true;
        }
        else if (other.name == "FourthCheckPoint"){
            meleeInstruction = true;
        }
        else if (other.tag == "HealthPotion"){
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
        else if (firstCorridor){
            instruction.text = "Pass over that potion there to collect. \n It will appear in your inventory.";
            firstCorridor = false;
        }
        else if (itemCollect){
            instruction.text = "You have just picked up a potion that increases health. To use it press 1.";
            itemCollect = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && secondCorridor == false){
            instruction.text = "Great now head to the next stage.";
            secondCorridor = true;
        }
        else if (jumpInstruction){
            instruction.text = "Press the Space Bar to jump over obstacles and holes. \n Don't worry if you fall, you will automatically respawn at your last checkpoint.";
            jumpInstruction = false;
        } 
        else if (fightInstruction){
            instruction.text = "Press J to shoot. Aim at that target. \n If you run out of mana collect some potions and to drink it press 3. ";
            fightInstruction = false;
        }
        else if (meleeInstruction){
            instruction.text = "To perform a melee attack click with your mouse. \n";
            meleeInstruction = false;
        } else if (Input.GetMouseButtonDown(0)){
            instruction.text = "Well done to exit this tutorial press p to pause. \n Then click quit to return to the main menu.";
        }
    }
}