using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ship : MonoBehaviour
{
    public Vector2 moveValue;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMove ( InputValue value ) {
        moveValue = value.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3 (moveValue.x , 0.0f , moveValue.y ) ;
        GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime );
    }
    
}
