using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerControl : MonoBehaviour
{
    CharacterController body;
    private Vector3 movement;
    private float speed = 7.0f;
    private float jump = 30.0f;
    private float gravity = 25.0f;
    private int health;
    public Text textbox;

    // Start is called before the first frame update
    void Start(){
        body = GetComponent<CharacterController>();
        movement = Vector3.zero;
        health = 100;
        textbox.text = "Health:" + health;
    }

    // Update is called once per frame
    void Update(){
        move();
        rotate();
    }

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

    void move(){
        if(body.isGrounded){
            movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            movement *= speed;
            movement = transform.rotation * movement;
            if(Input.GetButton("Jump")){
                movement.y = jump; 
            }
        }

        movement.y -= gravity * Time.deltaTime;
        body.Move(movement * Time.deltaTime);
    }

    void rotate(){
        transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X") * 4.0f,0.0f));
    }
}
