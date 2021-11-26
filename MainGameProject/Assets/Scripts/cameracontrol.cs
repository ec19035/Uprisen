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
    private float zoomTimes;
    private float rayLength;
    private bool hasZoomed;

    // Start is called before the first frame update
    void Start(){
        offset = transform.position - player.transform.position; // difference between player position and camera position
        // Intialize
        finalOffset = offset;
        zoomed = false;
        zoomTimes = 2.0f;
        rayLength = 5.0f;
        hasZoomed = false;
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
                rayLength = 0.0f;
            } else {
                zoomed = true;
                finalOffset.y = offset.y / 1.1f; // moves camera closer
                finalOffset.z = offset.z / 2;
                rayLength = 5.0f;
            }
        }
    }

    // checks movement and rotation of player and adjusts camera accordingly
    void LateUpdate(){
        rotate();
        // Lerp used to make camera movement more gradual
        transform.position = Vector3.Lerp(transform.position, (player.transform.position + finalOffset), 0.23f);
        transform.LookAt(player.transform.position); // makes sure camera is always looking at player
    }

    void fixCamera(){
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        Vector3 fwd = transform.TransformDirection(Vector3.forward); // raycast direction
        if (Physics.Raycast(transform.position, fwd, rayLength, layerMask)){ // casts a ray cast between player and camera
            if (zoomTimes < 3.8f){
                zoomTimes += 0.1f; // makes raycast smaller and zoom scale larger
                rayLength -= 0.1f;
            }
            finalOffset.z = offset.z / zoomTimes; // zooms in
            hasZoomed = true; // sets state tohas zoomed in
        } else {
            if (hasZoomed){ // resets value
                finalOffset.z = offset.z;
                zoomTimes = 2.0f;
                rayLength = 5.0f;
                hasZoomed = false;
            }
        }
    }

    // gets rotation for camera
    void rotate(){
        if (finalOffset.y + Input.GetAxis("Mouse Y") * 2.5f < 3.0f && finalOffset.y + Input.GetAxis("Mouse Y") * 2.5f > 0.1f)
            finalOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * 2.5f, Vector3.right) * finalOffset;
        finalOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 7.0f, Vector3.up) * finalOffset;
    }
}
