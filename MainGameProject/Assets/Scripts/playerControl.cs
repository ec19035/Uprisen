using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Use WASD to move player
// use Mouse to turn screen in the x axis

public class playerControl : MonoBehaviour
{
    CharacterController body; // used to control player body
    private Vector3 movement; // used to store movement vectors
    private float speed = 7.0f;
    private float jump = 10.0f;
    private float gravity = 25.0f;
    private int health; // used to keep track of player health 
    public Text textbox; // Used to display player health

    // Start is called before the first frame update
    void Start(){
        body = GetComponent<CharacterController>(); // used to get character controller applied to player
        movement = Vector3.zero; // intialize movement to zero
        health = 100;
        textbox.text = "Health:" + health;
    }

    // Update is called once per frame
    // Used to keep track of any movement inputs
    void Update(){
        move();
        rotate();
    }

    // checks if the player collides with an object ment for combat "Melee"
    // if the player is attacked by the enemy weapon
    // if health is zero scene is reset
    void OnTriggerEnter(Collider other){
        if (other.tag == "Melee"){
            health = health - 5;
            textbox.text = "Health:" + health;
            if(health <= 0){
                Destroy(gameObject);
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
    }

    // First checks if the player is on the ground
    // takes in the input from the keyboard and moves the player accordingly
    void move(){
        if(body.isGrounded){
            movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            movement *= speed; // increase speed to make movement more realistic
            movement = transform.rotation * movement; // multipy rotation by movement 
            if(Input.GetButton("Jump")){
                movement.y = jump; 
            }
            if(Input.GetButton("Dodge")){
                movement.z -= 10.0f;
            }
        } else {
            movement += new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")) * 0.1f;
        }

        movement.y -= gravity * Time.deltaTime; // constantly apply gravity to the player
        body.Move(movement * Time.deltaTime);
    }

    // used to rotate player on the x axis
    void rotate(){
        transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X") * 7.0f,0.0f));
    }
}
