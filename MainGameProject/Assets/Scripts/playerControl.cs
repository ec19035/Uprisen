using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    CharacterController body;
    private Vector3 movement;
    private float speed = 7.0f;
    private float jump = 7.0f;
    private float gravity = 25.0f;

    // Start is called before the first frame update
    void Start(){
        body = GetComponent<CharacterController>();
        movement = Vector3.zero;
    }

    // Update is called once per frame
    void Update(){
        move();
        rotate();
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
