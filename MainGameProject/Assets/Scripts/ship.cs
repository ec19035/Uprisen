using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ship : MonoBehaviour
{
    public Vector2 moveValue;
    public float speed = 500;
    public float jumphight = 2.0f;
    public float gravity = 20.0f;
    public bool jumping = false;
    public float rotation;
    // Start is called before the first frame update
    void Start(){
        
    }

    void OnCollisionStay(){
	    jumping = false;
    }

    // Update is called once per frame
    void Update(){
        Vector3 movement = new Vector3 (moveValue.x , 0.0f , moveValue.y);
        GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);
        rotation += Input.GetAxis("Horizontal");
        transform.Rotate(0,rotation,0);
        if(Input.GetKey("space") && jumping == false){
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumphight);
            jumping = true;
        }
    }

    void OnMove ( InputValue value ) {
        moveValue = value.Get<Vector2>();
    }
    
}
