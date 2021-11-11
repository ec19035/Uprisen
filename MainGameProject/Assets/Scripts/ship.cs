using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ship : MonoBehaviour
{
    Rigidbody body;
    public Vector2 moveValue;
    public float speed = 500;
    public float jumphight = 2.0f;
    public float gravity = 20.0f;
    public bool jumping = false;
    public float rotation;
    

    // Start is called before the first frame update
    void Start(){
        if (GetComponent<Rigidbody>()) {
            body = GetComponent<Rigidbody>();
        }
    }

    void OnCollisionStay(){
	    jumping = false;
    }

    // Update is called once per frame
    void Update(){
        Vector3 movement = new Vector3 (moveValue.x , 0.0f , moveValue.y);
        body.AddForce(movement * speed * Time.deltaTime);


        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotation * Time.deltaTime);            
        }

        if(Input.GetKey("space") && jumping == false){
            body.AddForce(Vector3.up * jumphight);
            jumping = true;
        }
    }

    void LateUpdate(){
        body.velocity = body.velocity * 0.9f;
    }

    void OnMove ( InputValue value ) {
        moveValue = value.Get<Vector2>();
    }
    
}
