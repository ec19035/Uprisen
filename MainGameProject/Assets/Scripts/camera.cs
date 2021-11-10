using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    public float horizontalSpeed = 1.0F;
    public float verticalSpeed = 1.0F;
    public float rotation;

    // Start is called before the first frame update
    void Start(){
        //offset = transform.position;
        offset = transform.position - player.transform.position;
    }

    void Update() {
        horizontalSpeed += Input.GetAxis("Mouse X") * 2;
        verticalSpeed -= Input.GetAxis("Mouse Y") * 2;
        transform.eulerAngles = new Vector3(verticalSpeed, horizontalSpeed, 0.0f);
    }

    // Update is called once per frame
    void LateUpdate(){
        transform.position = player.transform.position + offset;
        rotation += Input.GetAxis("Horizontal");
        //transform.Rotate(0,rotation,0);
    }

}
