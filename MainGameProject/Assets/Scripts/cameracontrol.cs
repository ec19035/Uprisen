using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to control camera, zoom in/out

public class cameracontrol : MonoBehaviour
{
    [SerializeField]
    private GameObject player; // gets player position 
    private bool zoomed; // checks if currently zoomed in
    private Vector3 offset; // stores difference between player position and camera position
    private Vector3 finalOffset; // takes into account any zoom

    // Start is called before the first frame update
    void Start(){
        offset = transform.position - player.transform.position; // difference between player position and camera position
        // Intialize
        finalOffset = offset;
        zoomed = false;
    }

    // Update is called once per frame
    void Update(){
        fixCamera();
        zoom();
    }

    // checks if the player has tried to zoom in
    void zoom(){
        if (Input.GetMouseButtonDown(1)){
            if (zoomed){
                zoomed = false;
                finalOffset.y = offset.y; // resets camera
                finalOffset.z = offset.z;
            } else {
                zoomed = true;
                finalOffset.y = offset.y / 2; // moves camera closer
                finalOffset.z = offset.z / 2;
            }
        }
    }

    void OnTriggerEnter(Collider other){
        Debug.Log("Alert!");
    }

    // checks movement and rotation of player and adjusts camera accordingly
    void LateUpdate(){
        rotate();
        // Lerp used to make camera movement more gradual
        transform.position = Vector3.Lerp(transform.position, (player.transform.position + finalOffset), 0.23f);
        transform.LookAt(player.transform.position); // makes sure camera is always looking at player
    }

    void fixCamera(){
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, 5f, layerMask)){
            finalOffset.z = offset.z / 2;
            print("There is something in front of the object!");
        } else {
            finalOffset.z = offset.z;
        }
    }

    // gets rotation for camera
    void rotate(){
        finalOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * 2.5f, Vector3.right) * finalOffset;
        finalOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 7.0f, Vector3.up) * finalOffset;
    }
}
