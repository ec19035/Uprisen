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

    // checks movement and rotation of player and adjusts camera accordingly
    void LateUpdate(){
        rotate();
        // Lerp used to make camera movement more gradual
        transform.position = Vector3.Lerp(transform.position, (player.transform.position + finalOffset), 0.23f);
        transform.LookAt(player.transform.position); // makes sure camera is always looking at player
    }

    // gets rotation for camera
    void rotate(){
        finalOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4.0f, Vector3.up) * finalOffset;
    }
}
