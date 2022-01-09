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
    private float jump = 7.0f;
    private float gravity = 25.0f;
    public float health; // used to keep track of player health 
    public Text textbox; // Used to display player health
    public Image HealthBar;

    // Start is called before the first frame update
    void Start(){
        body = GetComponent<CharacterController>(); // used to get character controller applied to player
        movement = Vector3.zero; // intialize movement to zero
        health = 100.0f;
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
            health = health - 5.0f;
            textbox.text = "Health:" + health;
            HealthBar.fillAmount = health/100.0f;
            if(health <= 0.0f){
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
            movement = transform.localRotation * movement; // multipy rotation by movement 
            if(Input.GetButton("Jump")){
                movement.y += jump; 
            }
        } else {
            if (transform.localRotation.y > 0.5 || transform.localRotation.y < -0.5){
                movement.x -= Input.GetAxis("Horizontal") * 0.1f; 
                //movement.z -= Input.GetAxis("Vertical") * 0.1f;
            } else {
                movement.x += Input.GetAxis("Horizontal") * 0.1f; 
                //movement.z += Input.GetAxis("Vertical") * 0.1f;
            }
        }

        if(Input.GetButton("Dodge")){
            if (transform.localRotation.y > 0.75 || transform.localRotation.y < -0.75){
                movement.z += 10.0f;
            } else if (transform.localRotation.y > 0.25){
                movement.x -= 10.0f;
            } else if (transform.localRotation.y < -0.25){
                movement.x += 10.0f;
            } else {
                movement.z -= 10.0f;
            }
        }
       
        movement.y -= gravity * Time.deltaTime; // constantly apply gravity to the player
        body.Move(movement * Time.deltaTime);
    }

    // used to rotate player on the x axis
    void rotate(){
        transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X") * 6.3f,0.0f));
    }
}
