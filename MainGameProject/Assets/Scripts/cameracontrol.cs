using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontrol : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private bool zoomed;
    private Vector3 offset;
    private Vector3 finalOffset;
    private bool collide;
    // Start is called before the first frame update
    void Start(){
        offset = transform.position - player.transform.position;
        finalOffset = offset;
        zoomed = false;
        collide = false;
    }

    void Update(){
        zoom();
    }

    void checkWall(){
        if (collide){
            finalOffset.z = offset.z * 1.2f;
            collide = false;
        } else {
            finalOffset.z = offset.z; 
        }
    }

    void OnCollisionEnter(Collision collision){
        collide = true;
    }

    void zoom(){
        if (Input.GetMouseButtonDown(1)){
            if (zoomed){
                zoomed = false;
                finalOffset.y = offset.y;
                finalOffset.z = offset.z;
            } else {
                zoomed = true;
                finalOffset.y = offset.y / 2;
                finalOffset.z = offset.z / 2;
            }
        }
    }

    void LateUpdate(){
        rotate();
        transform.position = Vector3.Lerp(transform.position, (player.transform.position + finalOffset), 0.23f);
        transform.LookAt(player.transform.position);
    }

    void rotate(){
        finalOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4.0f, Vector3.up) * finalOffset;
    }
}
